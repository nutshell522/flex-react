using FlexCore.Models;
using FlexCore.Models.DTOs;
using FlexCore.Models.Entities;
using FlexCore.Models.ViewModels.Client.Token;

namespace FlexCore.Services.Interfaces
{
    public interface IAuthService
    {
        Task<Result<string>> RegisterAsync(UserDto userDto);
        Task<Result<string>> ConfirmEmailAsync(string token, string email);
        Task<Result<AuthToken>> LoginAsync(UserDto userDto);
        Task<bool> IsEmailExits(string email);
        string GenerateJwtToken(UserEntity user);
    }
}
