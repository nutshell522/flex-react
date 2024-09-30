using FlexCore.Models;
using FlexCore.Models.Entities;

namespace FlexCore.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<UserEntity> Create(UserEntity user);
        Task<UserEntity> Update(UserEntity user);
        Task<UserEntity> GetByEmail(string email);
        Task<bool> IsEmailExist(string email);
        Task<bool> IsEmailConfirmed(string email);
    }
}
