namespace Core.ViewModels.Requests.User
{
    public record ResetPasswordRequest(string Email, string Token, string Password);
}
