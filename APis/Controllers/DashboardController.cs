using CleanBase.Core.Api.Controllers;
using CleanBase.Core.Services.Core.Base;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace APis.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ApiVersion("1.0")]
    [Authorize(
        AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme
    )]
    public class DashboardController : ApiControllerBase
    {
        public DashboardController(ICoreProvider coreProvider) : base(coreProvider)
        {
        }
    }
}
