using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModels.Responses.Identity
{
    public record AuthenticationPropertiesResponse(
     AuthenticationProperties Properties,
     string Provider
 );

}
