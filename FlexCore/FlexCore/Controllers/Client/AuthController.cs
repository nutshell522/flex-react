using FlexCore.Models.DTOs;
using FlexCore.Models.ViewModels.Client.User;
using FlexCore.Services.Interfaces;
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
    }
}
