namespace Core.ViewModels.Requests.User
{
    public record CreateUserRequest(string FirstName, string LastName, string Email, string Password);
}
