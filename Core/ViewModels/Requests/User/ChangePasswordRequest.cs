namespace Core.ViewModels.Requests.User
{
    public record ChangePasswordRequest(string OldPassword, string NewPassword, string Email);
}
