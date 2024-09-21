using Core.Identity.Email.Enums;
using Core.Identity.Email.Models;

namespace Core.Identity.Email.Interfaces
{
    public interface IEmailClassifier
    {
        bool Classified(EmailType type);
        ClassifiedEmail GetEmail(IDictionary<string, string> parameters);
    }
}
