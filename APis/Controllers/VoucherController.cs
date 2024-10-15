using CleanBase.Core.Api.Controllers;
using CleanBase.Core.Domain.Generic;
using CleanBase.Core.Services.Core.Base;
using CleanBase.Core.ViewModels.Response;
using CleanBase.Core.ViewModels.Response.Generic;
using Core.Entities;
using Core.Identity.Constants.Authorization;
using Core.Services;
using Core.ViewModels.Requests.Voucher;
using Core.ViewModels.Responses.Voucher;
using Hangfire;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace APis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = false)]
    public partial class VoucherController : CRUDBaseController<Voucher, VoucherRequest, VoucherResponse, VoucherGetAllRequest, IVoucherService>
    {
        public VoucherController(ICoreProvider coreProvider, IVoucherService service) : base(coreProvider, service)
        {
        }

        [HttpGet("{id}")]
        [Authorize(
            AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
            Policy = ApiPolicy.AdminReadAccess
        )]
        [ProducesResponseType(typeof(ActionResponse<VoucherResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(FailActionResponse), (int)HttpStatusCode.BadRequest)]
        public virtual async Task<IActionResult> GetById(Guid id)
        {
            return await GetByIdInternal(id);
        }

        [HttpPost("get-basic")]
        [Authorize(
            AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
            Policy = ApiPolicy.AdminReadAccess
        )]
        [ProducesResponseType(typeof(ActionResponse<ListResult<VoucherResponse>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(FailActionResponse), (int)HttpStatusCode.BadRequest)]
        public virtual async Task<IActionResult> GetAll([FromBody] VoucherGetAllRequest request)
        {
            return await GetAllInternal(request);
        }


        [HttpPost]
        [Authorize(
            AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
            Policy = ApiPolicy.AdminWriteAccess
        )]
        [ProducesResponseType(typeof(ActionResponse<VoucherResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(FailActionResponse), (int)HttpStatusCode.BadRequest)]
        public virtual async Task<IActionResult> CreateOrUpdate([FromBody] VoucherRequest entity)
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

        [HttpPost("check")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ActionResponse<VoucherResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(FailActionResponse), (int)HttpStatusCode.BadRequest)]
        public virtual async Task<IActionResult> CheckValidVoucher(CheckValidVoucherRequest request)
        {
            var result = await Service.CheckValidVoucherAsync(request);
            return CreateSuccessResult(result);
        }

        [HttpGet("get-voucher-by-code")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ActionResponse<VoucherResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(FailActionResponse), (int)HttpStatusCode.BadRequest)]
        public virtual async Task<IActionResult> GetVoucherByCode([FromQuery]string request)
        {
            var result = await Service.GetVoucherByCode(request);
            return CreateSuccessResult(result);
        }
    }
}