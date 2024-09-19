using Core.ViewModels.Responses.Identity;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
