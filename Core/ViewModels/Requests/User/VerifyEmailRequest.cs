namespace Core.ViewModels.Requests.User
{
    public record VerifyEmailRequest(string Email, string Token);
}
