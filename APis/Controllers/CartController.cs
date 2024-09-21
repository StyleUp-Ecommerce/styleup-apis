using CleanBase.Core.Api.Controllers;
using CleanBase.Core.Domain.Generic;
using CleanBase.Core.Services.Core.Base;
using CleanBase.Core.ViewModels.Response;
using CleanBase.Core.ViewModels.Response.Generic;
using Core.Entities;
using Core.Services;
using Core.ViewModels.Requests.Cart;
using Core.ViewModels.Responses.Cart;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
namespace APis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = false)]
    public partial class CartController : CRUDBaseController<Cart, CartRequest, CartResponse, GetAllCartRequest, ICartService>
    {
        public CartController(ICoreProvider coreProvider, ICartService service) : base(coreProvider, service)
        {
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ActionResponse<CartResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(FailActionResponse), (int)HttpStatusCode.BadRequest)]
        public virtual async Task<IActionResult> GetById(Guid id)
        {
            return await GetByIdInternal(id);
        }

        [Authorize(
            AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme
        )]
        [HttpPost("get-basic")]
        [ProducesResponseType(typeof(ActionResponse<ListResult<CartResponse>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(FailActionResponse), (int)HttpStatusCode.BadRequest)]
        public virtual async Task<IActionResult> GetAll([FromBody] GetAllCartRequest request)
        {
            return await GetAllInternal(request);
        }


        [HttpPost]
        [ProducesResponseType(typeof(ActionResponse<CartResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(FailActionResponse), (int)HttpStatusCode.BadRequest)]
        public virtual async Task<IActionResult> CreateOrUpdate([FromBody] CartRequest entity)
        {
            return await CreateOrUpdateInternal(entity);
        }


        [HttpPost("delete/{id}")]
        [ProducesResponseType(typeof(ActionResponse<bool>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(FailActionResponse), (int)HttpStatusCode.BadRequest)]
        public virtual async Task<IActionResult> DeActive(Guid id)
        {
            return await DeActiveInternal(id);
        }
    }
}
