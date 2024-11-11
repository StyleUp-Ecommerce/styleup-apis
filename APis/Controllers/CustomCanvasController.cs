using CleanBase.Core.Api.Controllers;
using CleanBase.Core.Domain.Generic;
using CleanBase.Core.Services.Core.Base;
using CleanBase.Core.ViewModels.Response;
using CleanBase.Core.ViewModels.Response.Generic;
using Core.Entities;
using Core.Identity.Constants.Authorization;
using Core.Services;
using Core.ViewModels.Requests.CustomCanvas;
using Core.ViewModels.Responses.CustomCanvas;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
namespace APis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = false)]
    public partial class CustomCanvasController : CRUDBaseController<CustomCanvas, CustomCanvasRequest, CustomCanvasResponse, GetAllCustomCanvasRequest, ICustomCanvasService>
    {
        public CustomCanvasController(ICoreProvider coreProvider, ICustomCanvasService service) : base(coreProvider, service)
        {
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ActionResponse<CustomCanvasResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(FailActionResponse), (int)HttpStatusCode.BadRequest)]
        public virtual async Task<IActionResult> GetById(Guid id)
        {
            return await GetByIdInternal(id);
        }

        [HttpPost("get-basic")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ActionResponse<ListResult<CustomCanvasResponse>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(FailActionResponse), (int)HttpStatusCode.BadRequest)]
        public virtual async Task<IActionResult> GetAll([FromBody] GetAllCustomCanvasRequest request)
        {
            return await GetAllInternal(request);
        }


        [HttpPost]
        //[Authorize(
        //    AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
        //    Policy = ApiPolicy.WriteAccess
        //)]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ActionResponse<CustomCanvasResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(FailActionResponse), (int)HttpStatusCode.BadRequest)]
        public virtual async Task<IActionResult> CreateOrUpdate([FromBody] CustomCanvasRequest entity)
        {
            return await CreateOrUpdateInternal(entity);
        }


        [HttpPost("delete/{id}")]
        [Authorize(
            AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
            Policy = ApiPolicy.AdminDeleteAccess
        )]
        [ProducesResponseType(typeof(ActionResponse<bool>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(FailActionResponse), (int)HttpStatusCode.BadRequest)]
        public virtual async Task<IActionResult> DeActive(Guid id)
        {
            return await DeActiveInternal(id);
        }


        [HttpGet("selft-edit/{id}")]
        [Authorize(
            AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
            Policy = ApiPolicy.ReadAccess

        )]
        [ProducesResponseType(typeof(ActionResponse<Guid>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(FailActionResponse), (int)HttpStatusCode.BadRequest)]
        public virtual async Task<IActionResult> CustomNewTemplate(Guid id)
        {
            var result = await Service.CustomNewTemplate(id);
            return CreateSuccessResult(result);
        }
    }
}
