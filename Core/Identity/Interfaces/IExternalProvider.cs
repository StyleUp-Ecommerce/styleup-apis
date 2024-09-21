using Core.ViewModels.Responses.Identity;
using Microsoft.AspNetCore.Http;

namespace Core.Identity.Interfaces
{
    public interface IExternalProvider
    {
        bool Classify(string returnUrl);

        AuthenticationPropertiesResponse GetAuthenticationProperties(
            string returnUrl,
            HttpRequest request
        );
    }
}
