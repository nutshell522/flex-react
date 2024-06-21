namespace FlexCore.Models.ViewModels.Client.Token
{
    public class AuthToken
    {
        public string Token { get; set; }
        public static AuthToken Create(string token)
        {
            return new AuthToken
            {
                Token = token
            };
        }
    }
}
