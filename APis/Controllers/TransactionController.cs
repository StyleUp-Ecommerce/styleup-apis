using CleanBase.Core.Api.Controllers;
using CleanBase.Core.Domain.Generic;
using CleanBase.Core.Services.Core.Base;
using CleanBase.Core.ViewModels.Response;
using CleanBase.Core.ViewModels.Response.Generic;
using Core.Entities;
using Core.Identity.Constants.Authorization;
using Core.Services;
using Core.ViewModels.Requests.Transaction;
using Core.ViewModels.Responses.Transaction;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace APis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = false)]
    public class TransactionController : CRUDBaseController<Transaction, TransactionRequest, TransactionResponse, GetAllTransactionRequest, ITransactionService>
    {
        public TransactionController(ICoreProvider coreProvider, ITransactionService service) : base(coreProvider, service)
        {
        }

        [HttpGet("{id}")]
        [Authorize(
            AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
            Policy = ApiPolicy.AdminReadAccess
        )]
        [ProducesResponseType(typeof(ActionResponse<TransactionResponse>), (int)HttpStatusCode.OK)]
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
        [ProducesResponseType(typeof(ActionResponse<ListResult<TransactionResponse>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(FailActionResponse), (int)HttpStatusCode.BadRequest)]
        public virtual async Task<IActionResult> GetAll([FromBody] GetAllTransactionRequest request)
        {
            return await GetAllInternal(request);
        }


        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ActionResponse<CreateTransactionResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(FailActionResponse), (int)HttpStatusCode.BadRequest)]
        public virtual async Task<IActionResult> CreateOrUpdate([FromBody] TransactionRequest transaction)
        {

            var result = await Service.CreateTransactionRequest(transaction);
            return CreateSuccessResult(result);
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

        [HttpGet("confirm")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ActionResponse<bool>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(FailActionResponse), (int)HttpStatusCode.BadRequest)]
        public virtual async Task<IActionResult> ComfirmTransaction([FromQuery] string vnp_TxnRef, [FromQuery] string vnp_SecureHash)
        {
            string queryString = Request.QueryString.Value.Substring(1);
            bool isValid = Service.ValidateSignature(queryString, vnp_SecureHash);

            if (isValid)
            {
                var transaction = await Service.ConfirmTransaction(queryString);
                return Ok(transaction);
            }
            return BadRequest("Invalid signature");
        }



    }
}
