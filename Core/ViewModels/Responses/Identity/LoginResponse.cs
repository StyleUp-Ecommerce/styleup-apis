namespace Core.ViewModels.Responses.Identity
{
    public class LoginResponse
    {
        public string AccessToken { get; set; }

        public int ExpiresIn { get; set; }

        public string TokenType { get; set; }

        public string RefreshToken { get; set; }

        public Core.Entities.User User { get; set; }
    }
}
