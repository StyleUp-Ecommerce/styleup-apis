﻿using CleanBase.Core.Api.Controllers;
using CleanBase.Core.Domain.Generic;
using CleanBase.Core.Services.Core.Base;
using CleanBase.Core.ViewModels.Response;
using CleanBase.Core.ViewModels.Response.Generic;
using Core.Entities;
using Core.Services;
using Core.ViewModels.Requests.TemplateCanvas;
using Core.ViewModels.Responses.TemplateCanvas;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
namespace APis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = false)]
    public partial class TemplateCanvasController : CRUDBaseController<TemplateCanvas, TemplateCanvasRequest, TemplateCanvasResponse, GetAllTemplateCanvasRequest, ITemplateCanvasService>
    {
        public TemplateCanvasController(ICoreProvider coreProvider, ITemplateCanvasService service) : base(coreProvider, service)
        {
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ActionResponse<TemplateCanvasResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(FailActionResponse), (int)HttpStatusCode.BadRequest)]
        public virtual async Task<IActionResult> GetById(Guid id)
        {
            return await GetByIdInternal(id);
        }

        [HttpGet("products/{id}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ActionResponse<GetTemplateCanvasProductResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(FailActionResponse), (int)HttpStatusCode.BadRequest)]
        public virtual async Task<IActionResult> GetProductsByTemplateId(Guid id)
        {
            var result = await Service.GetTemplateCanvasProductAsync(id);
            return CreateSuccessResult(result);
        }

        [HttpPost("get-basic")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ActionResponse<ListResult<TemplateCanvasResponse>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(FailActionResponse), (int)HttpStatusCode.BadRequest)]
        public virtual async Task<IActionResult> GetAll([FromBody] GetAllTemplateCanvasRequest request)
        {
            var result = await this.Service.GetAllTemplateCanvasAsync(request);
            return CreateSuccessResult(result);
        }

        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ActionResponse<TemplateCanvasResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(FailActionResponse), (int)HttpStatusCode.BadRequest)]
        public virtual async Task<IActionResult> CreateOrUpdate([FromBody] TemplateCanvasRequest entity)
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
