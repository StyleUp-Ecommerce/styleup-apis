using CleanBase.Core.Identity.Email.Constants;
using Core.Identity.Email.Enums;
using Core.Identity.Email.Interfaces;
using Core.Identity.Email.Models;

namespace Domain.Identity.Email
{
    public class ResetPasswordTemplate : IEmailClassifier
    {
        public bool Classified(EmailType type) => EmailType.ResetPassword == type;

        public ClassifiedEmail GetEmail(IDictionary<string, string> bodyParameters)
        {
            ClassifiedEmail classified =
                new()
                {
                    Subject = EmailSubject.PasswordReset,
                    Body = EmailBody.PasswordReset(bodyParameters["fullName"], bodyParameters["url"])
                };

            return classified;
        }
    }
}
