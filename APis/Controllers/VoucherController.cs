using CleanBase.Core.Api.Controllers;
using CleanBase.Core.Domain.Generic;
using CleanBase.Core.Services.Core.Base;
using CleanBase.Core.ViewModels.Response;
using CleanBase.Core.ViewModels.Response.Generic;
using Core.Entities;
using Core.Services;
using Core.ViewModels.Requests.Voucher;
using Core.ViewModels.Responses.Voucher;
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
        [AllowAnonymous]
        [ProducesResponseType(typeof(ActionResponse<VoucherResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(FailActionResponse), (int)HttpStatusCode.BadRequest)]
        public virtual async Task<IActionResult> GetById(Guid id)
        {
            return await GetByIdInternal(id);
        }

        [HttpPost("get-basic")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ActionResponse<ListResult<VoucherResponse>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(FailActionResponse), (int)HttpStatusCode.BadRequest)]
        public virtual async Task<IActionResult> GetAll([FromBody] VoucherGetAllRequest request)
        {
            return await GetAllInternal(request);
        }


        [HttpPost]
        [AllowAnonymous]

        [ProducesResponseType(typeof(ActionResponse<VoucherResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(FailActionResponse), (int)HttpStatusCode.BadRequest)]
        public virtual async Task<IActionResult> CreateOrUpdate([FromBody] VoucherRequest entity)
        {

            return await CreateOrUpdateInternal(entity);
        }


        [HttpPost("delete/{id}")]
        [AllowAnonymous]

        [ProducesResponseType(typeof(ActionResponse<bool>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(FailActionResponse), (int)HttpStatusCode.BadRequest)]
        public virtual async Task<IActionResult> DeActive(Guid id)
        {
            return await DeActiveInternal(id);
        }
    }
}