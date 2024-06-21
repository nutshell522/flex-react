using System.ComponentModel.DataAnnotations;

namespace FlexCore.Models.Entities
{
    public class UserEntity
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string EmailConfirmed { get; set; } = string.Empty;

        public bool IsComfirmed { get; set; } = false;
    }
}
