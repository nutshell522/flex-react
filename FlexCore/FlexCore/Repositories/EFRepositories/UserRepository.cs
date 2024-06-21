using FlexCore.Data;
using FlexCore.Models;
using FlexCore.Models.Entities;
using FlexCore.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FlexCore.Repositories.EFRepositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _db;

        public UserRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<UserEntity> Create(UserEntity user)
        {
            user.Id = Guid.NewGuid();
            _db.Users.Add(user);
            await _db.SaveChangesAsync();

            var createdUser = await _db.Users.FirstOrDefaultAsync(u => u.Email == user.Email);
            if (createdUser == null) throw new Exception("User not created");

            return createdUser;
        }

        public async Task<UserEntity> Update(UserEntity user)
        {
            _db.Users.Update(user);
            await _db.SaveChangesAsync();

            var updatedUser = await _db.Users.FirstOrDefaultAsync(u => u.Email == user.Email);
            if (updatedUser == null) throw new Exception("User not updated");

            return updatedUser;
        }
        public Task<bool> IsEmailExist(string email)
        {
            return _db.Users.AnyAsync(u => u.Email == email);
        }

        public async Task<UserEntity> GetByEmail(string email)
        {
            return await _db.Users.FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}
