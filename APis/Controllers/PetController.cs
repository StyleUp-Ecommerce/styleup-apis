using CleanBase.Core.Api.Controllers;
using CleanBase.Core.Domain.Generic;
using CleanBase.Core.Services.Core.Base;
using CleanBase.Core.Validators.Generic;
using CleanBase.Core.ViewModels.Response;
using Domain.Validators;
using CleanBase.Core.ViewModels.Response.Generic;
using Core.Entities;
using Core.Services;
using Core.ViewModels.Requests.User;
using Core.ViewModels.Responses.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Core.ViewModels.Requests.Pet;

namespace APis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = false)]
    public partial class PetController : CRUDBaseController<Pet, PetRequest, UserResponse, PetGetAllRequest, IPetService>
    {
        private readonly IValidator<PetRequest> _userValidator;
        public PetController(ICoreProvider coreProvider, IPetService service, IValidator<PetRequest> userValidator) : base(coreProvider, service)
        {
            _userValidator = userValidator;
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ActionResponse<UserResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(FailActionResponse), (int)HttpStatusCode.BadRequest)]
        public virtual async Task<IActionResult> GetById(Guid id)
        {
            return await GetByIdInternal(id);
        }

        [HttpPost("get-basic")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ActionResponse<ListResult<UserResponse>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(FailActionResponse), (int)HttpStatusCode.BadRequest)]
        public virtual async Task<IActionResult> GetAll([FromBody] PetGetAllRequest request)
        {
            return await GetAllInternal(request);
        }


        [HttpPost]
        [AllowAnonymous]

        [ProducesResponseType(typeof(ActionResponse<UserResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(FailActionResponse), (int)HttpStatusCode.BadRequest)]
        public virtual async Task<IActionResult> CreateOrUpdate([FromBody] PetRequest entity)
        {
            _userValidator.ThrowIfValidationFails(entity);

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

        [HttpGet("get-user-message/{id}")]
        [AllowAnonymous]

        [ProducesResponseType(typeof(ActionResponse<UserResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(FailActionResponse), (int)HttpStatusCode.BadRequest)]
        public virtual async Task<IActionResult> GetUserMessage(Guid id)
        {
            var result = await this.Service.GetUserRandomMessage(id);
            return CreateSuccessResult(result);
        }
    }
}

