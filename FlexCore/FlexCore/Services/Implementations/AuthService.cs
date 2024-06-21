using AutoMapper;
using FlexCore.Data;
using FlexCore.Models;
using FlexCore.Models.DTOs;
using FlexCore.Models.Entities;
using FlexCore.Models.ViewModels.Client.Token;
using FlexCore.Repositories.Interfaces;
using FlexCore.Services.Interfaces;
using FlexCore.Utils.EmailSender;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FlexCore.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly IEmailSender _emailSender;
        private readonly IUserRepository _repo;
        private readonly IMapper _mapper;

        public AuthService(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IConfiguration configuration, IEmailSender emailSender,IUserRepository repo, IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _emailSender = emailSender;
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<Result<string>> RegisterAsync(UserDto userDto)
        {
            if (_repo.IsEmailExist(userDto.Email).Result) return Result<string>.Failure("Email已被註冊過");

            var identityUser = new IdentityUser { Email = userDto.Email, UserName = userDto.Name };
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(identityUser);

            var user = new UserEntity
            {
                Name = userDto.Name,
                Email = userDto.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(userDto.Password),
                EmailConfirmed = token,
            };

            await _repo.Create(user);

            var confirmationLink = $"請點擊<a href=\"{_configuration["AppUrl"]}/api/auth/confirmemail?token={token}&email={user.Email}\">此處</a>完成帳號註冊。";
            await _emailSender.SendEmailAsync(user.Email, "Email Confirmation", confirmationLink);

            return Result<string>.Success("註冊成功，請去收取信件");
        }
        [Authorize]
        public async Task<Result<string>> ConfirmEmailAsync(string token, string email)
        {
            var user = await _repo.GetByEmail(email);

            if (user == null) return Result<string>.Failure("驗證失敗");
            if (user.EmailConfirmed != token) return Result<string>.Failure("驗證失敗");

            user.EmailConfirmed = string.Empty;
            user.IsComfirmed = true;

            await _repo.Update(user);

            return Result<string>.Success("驗證成功");
        }

        public async Task<Result<AuthToken>> LoginAsync(UserDto userDto)
        {
            var user = await _repo.GetByEmail(userDto.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(userDto.Password, user.Password) || !user.IsComfirmed)
                return Result<AuthToken>.Failure("用戶名或密碼錯誤");

            return Result<AuthToken>.Success(AuthToken.Create(GenerateJwtToken(user)));
        }

        public async Task<bool> IsEmailExits(string email)
        {
            return await _repo.IsEmailExist(email);
        }

        public string GenerateJwtToken(UserEntity user)
        {
            // 創建一個 JwtSecurityTokenHandler 來處理 JWT 令牌
            var tokenHandler = new JwtSecurityTokenHandler();

            // 從配置中獲取 JWT 密鑰並轉換為字節數組
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);

            // 定義一個 SecurityTokenDescriptor 來描述令牌的屬性和內容
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                // 定義令牌的主體，其中包含用戶的聲明（Claims）
                Subject = new ClaimsIdentity(new[]
                {
            // 添加一個聲明，聲明類型為 Name，值為用戶的名字
            new Claim(ClaimTypes.Name, user.Name),
            // 添加一個聲明，聲明類型為 Email，值為用戶的電子郵件地址
            new Claim(ClaimTypes.Email, user.Email),
            // 添加一個自定義聲明，聲明類型為 FullName，值為用戶的名字
            new Claim("FullName", user.Name)
        }),
                // 設置令牌的過期時間
                Expires = DateTime.UtcNow.AddMinutes(int.Parse(_configuration["Jwt:ExpiryMinutes"])),
                // 設置令牌的簽名憑證，使用 HMAC SHA256 算法和密鑰
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                // 設置令牌的發行者
                Issuer = _configuration["Jwt:Issuer"],
                // 設置令牌的受眾
                Audience = _configuration["Jwt:Audience"]
            };

            // 使用 tokenHandler 創建一個 JWT 令牌
            var token = tokenHandler.CreateToken(tokenDescriptor);

            // 將 JWT 令牌寫為字串並返回
            return tokenHandler.WriteToken(token);
        }
    }
}
