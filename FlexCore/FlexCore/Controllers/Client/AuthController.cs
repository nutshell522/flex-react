using FlexCore.Models;
using FlexCore.Models.DTOs;
using FlexCore.Models.ViewModels.Client.User;
using FlexCore.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlexCore.Controllers.Client
{
    [Route("api/client/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// 註冊
        /// </summary>
        /// <param name="registerVM">註冊資料</param>
        /// <returns></returns>
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterVM registerVM)
        {
            var userDto = new UserDto
            {
                Name = registerVM.Name,
                Email = registerVM.Email,
                Password = registerVM.Password
            };
            var result = await _authService.RegisterAsync(userDto);

            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        /// <summary>
        /// 用戶登入 API，驗證使用者的帳號和密碼，並返回身份驗證 Token。
        /// </summary>
        /// <param name="loginVM">包含用戶登入資料的視圖模型 (ViewModel)。</param>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginVM loginVM)
        {
            var userDto = new UserDto
            {
                Email = loginVM.Email,
                Password = loginVM.Password
            };
            var result = await _authService.LoginAsync(userDto);

            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        /// <summary>
        /// 驗證用戶的電子郵件地址，根據提供的驗證令牌和電子郵件，進行帳號驗證。
        /// </summary>
        /// <param name="token">用戶的電子郵件驗證令牌。</param>
        /// <param name="email">要驗證的用戶電子郵件。</param>
        /// <returns></returns>
        [HttpGet("confirmEmail")]
        public async Task<IActionResult> ConfirmEmail([FromQuery] string token, [FromQuery] string email)
        {
            var result = await _authService.ConfirmEmailAsync(token, email);

            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        /// <summary>
        /// 檢查電子郵件的註冊和驗證狀態。
        /// </summary>
        /// <param name="email">要檢查的用戶電子郵件。</param>
        /// <returns></returns>
        [HttpGet("checkEmailStatus")]
        public async Task<IActionResult> CheckEmailStatus([FromQuery] string email)
        {
            var result = await _authService.CheckEmailStatusAsync(email);

            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }


    }
}
