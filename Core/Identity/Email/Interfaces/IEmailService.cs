using Core.Entities;
using Core.Identity.Email.Enums;
using Core.Identity.Email.Models;

namespace Core.Identity.Email.Interfaces
{
    public interface IEmailService
    {
        Task SendAsync(EmailType type, string email, Dictionary<string, string> paramters);
        ClassifiedEmail Classify(EmailType type, Dictionary<string, string> paramters);
        Dictionary<string, string> GenerateEmailConfirmationParameters(User member, string token);
        Dictionary<string, string> GenerateResetPasswordParameters(User member, string token);
    }
}
