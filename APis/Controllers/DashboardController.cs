using CleanBase.Core.Api.Controllers;
using CleanBase.Core.Services.Core.Base;
using CleanBase.Core.ViewModels.Response;
using CleanBase.Core.ViewModels.Response.Generic;
using Core.Identity.Constants.Authorization;
using Core.Services;
using Core.ViewModels.Responses.DashBoard;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace APis.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ApiVersion("1.0")]
    [Authorize(
        AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        Policy = ApiPolicy.AdminReadAccess
    )]
    public class DashboardController : ApiControllerBase
    {
        private readonly IDashboardService _service;
        public DashboardController(ICoreProvider coreProvider, IDashboardService dashboardService) : base(coreProvider)
        {
            _service = dashboardService;
        }

        [HttpGet("")]
        [ProducesResponseType(typeof(ActionResponse<DashBoardResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(FailActionResponse), (int)HttpStatusCode.BadRequest)]
        public virtual async Task<IActionResult> GetDashboardData()
        {
            var result = await this._service.GetHomeDashBoardData();
            return CreateSuccessResult(result);
        }
    }
}
