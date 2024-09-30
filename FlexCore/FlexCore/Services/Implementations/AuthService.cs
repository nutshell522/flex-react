using AutoMapper;
using FlexCore.Data;
using FlexCore.Models;
using FlexCore.Models.DTOs;
using FlexCore.Models.Entities;
using FlexCore.Models.Enum;
using FlexCore.Models.ViewModels.Client.Token;
using FlexCore.Repositories.Interfaces;
using FlexCore.Services.Interfaces;
using FlexCore.Utils.EmailSender;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using System.Net;
using Microsoft.IdentityModel.JsonWebTokens;
using FlexCore.Models.ViewModels.Client.User;

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

        public AuthService(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IConfiguration configuration, IEmailSender emailSender, IUserRepository repo, IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _emailSender = emailSender;
            _repo = repo;
            _mapper = mapper;
        }

        /// <summary>
        /// 註冊新用戶，並發送驗證電子郵件。會生成一個驗證令牌並將其通過電子郵件發送給用戶。
        /// 在生成的 URL 中，令牌會經過 URL 編碼，避免因特殊符號導致的驗證問題。
        /// </summary>
        /// <param name="userDto">包含用戶註冊資料的 DTO。</param>
        /// <returns>
        /// 如果註冊成功，返回成功訊息和狀態；如果電子郵件已存在，返回失敗訊息。
        /// </returns>
        public async Task<Result<string>> RegisterAsync(UserDto userDto)
        {
            // 檢查電子郵件是否已存在
            if (_repo.IsEmailExist(userDto.Email).Result) return Result<string>.Failure("Email已被註冊過");

            // 創建 IdentityUser 並生成確認電子郵件的令牌
            var identityUser = new IdentityUser { Email = userDto.Email, UserName = userDto.Name };
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(identityUser);

            // 將用戶資料存入資料庫
            var user = new UserEntity
            {
                Name = userDto.Name,
                Email = userDto.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(userDto.Password),
                EmailConfirmed = token
            };

            await _repo.Create(user);

            // 對 token 進行 URL 編碼以避免特殊符號問題 (例如: + 號被解讀為空格)
            var encodedToken = WebUtility.UrlEncode(token);

            // 生成確認連結，將編碼後的 token 加入 URL 中
            var confirmationLink = $"請點擊<a href=\"{_configuration["AppUrl"]}/api/client/auth/confirmemail?token={encodedToken}&email={user.Email}\">此處</a>完成帳號註冊。";

            // 發送確認電子郵件
            await _emailSender.SendEmailAsync(user.Email, "Flex 帳號驗證信件", confirmationLink);

            return Result<string>.Success("註冊成功，請去收取信件");
        }


        /// <summary>
        /// 驗證用戶的電子郵件地址，並確認用戶帳號的有效性。
        /// </summary>
        /// <param name="token">電子郵件驗證的令牌。</param>
        /// <param name="email">用戶的電子郵件地址。</param>
        /// <returns>
        /// 如果驗證成功，返回成功訊息；如果驗證失敗，返回失敗訊息。
        /// </returns>
        [Authorize]
        public async Task<Result<string>> ConfirmEmailAsync(string token, string email)
        {
            var user = await _repo.GetByEmail(email);

            if (user == null || user.EmailConfirmed != token) return Result<string>.Failure("驗證失敗");

            user.EmailConfirmed = string.Empty;
            user.IsComfirmed = true;

            await _repo.Update(user);

            return Result<string>.Success("帳號驗證成功");
        }

        /// <summary>
        /// 用戶登入，驗證電子郵件和密碼，並生成 JWT Token。
        /// </summary>
        /// <param name="userDto">包含用戶登入資料的 DTO。</param>
        /// <returns>
        /// 如果登入成功，返回包含 JWT Token 的結果；如果登入失敗，返回錯誤訊息。
        /// </returns>
        public async Task<Result<AuthToken>> LoginAsync(UserDto userDto)
        {
            var user = await _repo.GetByEmail(userDto.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(userDto.Password, user.Password) || !user.IsComfirmed)
                return Result<AuthToken>.Failure("用戶名或密碼錯誤");

            var vm = new AuthToken
            {
                Token = GenerateJwtToken(user),
                User = new UserVM
                {
                    Id = user.Id.ToString(),
                    Name = user.Name,
                    Email = user.Email
                }
            };

            return Result<AuthToken>.Success(vm);
        }

        /// <summary>
        /// 驗證電子郵件的註冊與確認狀態。
        /// </summary>
        /// <param name="email">要驗證的電子郵件。</param>
        /// <returns>
        /// 返回電子郵件的狀態，可能是未註冊、未確認或已確認。
        /// </returns>
        public async Task<Result<EmailStatus>> CheckEmailStatusAsync(string email)
        {
            if (await _repo.IsEmailExist(email))
            {
                if (await _repo.IsEmailConfirmed(email))
                {
                    return Result<EmailStatus>.Success(EmailStatus.Confirmed);
                }
                else
                {
                    return Result<EmailStatus>.Success(EmailStatus.NotConfirmed);
                }
            }
            else
            {
                return Result<EmailStatus>.Success(EmailStatus.Unregistered);
            }
        }

        /// <summary>
        /// 生成用戶的 JWT Token，包含用戶的相關聲明和有效期。
        /// </summary>
        /// <param name="user">用戶實體，包含 JWT 令牌需要的資料。</param>
        /// <returns>返回生成的 JWT Token 字串。</returns>
        public string GenerateJwtToken(UserEntity user)
        {
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
            new Claim(ClaimTypes.Name, user.Name),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim("FullName", user.Name)
        }),
                Expires = DateTime.UtcNow.AddMinutes(int.Parse(_configuration["Jwt:ExpiryMinutes"])),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"]
            };

            // Generate a JWT, than get the serialized Token result (string)
            var tokenHandler = new JsonWebTokenHandler();
            var serializeToken = tokenHandler.CreateToken(tokenDescriptor);

            return serializeToken;
        }
    }
}
