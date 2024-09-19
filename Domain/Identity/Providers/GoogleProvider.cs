using Core.Identity.Constants;
using Core.Identity.Constants.Authorization;
using Core.Identity.Interfaces;
using Core.ViewModels.Responses.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Identity.Providers
{
    public class GoogleProvider : IExternalProvider
    {
        public bool Classify(string returnUrl) =>
            returnUrl.Contains(IdentityProvider.Google, StringComparison.OrdinalIgnoreCase);

        public AuthenticationPropertiesResponse GetAuthenticationProperties(
            string returnUrl,
            HttpRequest request
        )
        {
            var properties = new AuthenticationProperties
            {
                RedirectUri = $"{request.Scheme}://{request.Host}{IdentityDefaults.CallbackUrl}",
                Items = { { "returnUrl", returnUrl }, { "scheme", IdentityProvider.Google } }
            };

            var response = new AuthenticationPropertiesResponse(properties, IdentityProvider.Google);

            return response;
        }
    }

}
