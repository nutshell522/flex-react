using FlexCore.Models.ViewModels.Client.User;

namespace FlexCore.Models.ViewModels.Client.Token
{
    public class AuthToken
    {
        public string Token { get; set; }
        public UserVM User { get; set; }
        public static AuthToken Create(string token)
        {
            return new AuthToken
            {
                Token = token
            };
        }
    }
}
