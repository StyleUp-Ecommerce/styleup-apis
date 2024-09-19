using Core.Identity.Email.Models;
using Azure.Communication.Email;

namespace Domain.Extensions.Email
{
    public static class MappingExtension
    {
        /// <summary>
        /// Mapps the classified <paramref name="email"/> to Azure EmailContent object.
        /// </summary>
        /// <param name="email">The <see cref="ClassifiedEmail"/> from which to select email content.</param>
        /// <returns>An <see cref="EmailContent"/> object containing the selected email content.</returns>
        public static EmailContent SelectEmailContent(this ClassifiedEmail email) =>
            new EmailContent(email.Subject) { Html = email.Body };
    }
}
