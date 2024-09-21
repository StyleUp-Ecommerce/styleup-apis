using Microsoft.AspNetCore.Authentication;

namespace Core.ViewModels.Responses.Identity
{
    public record AuthenticationPropertiesResponse(
     AuthenticationProperties Properties,
     string Provider
 );

}
