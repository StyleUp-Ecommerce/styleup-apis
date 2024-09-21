using CleanBase.Core.Api.Controllers;
using CleanBase.Core.Services.Core.Base;
using CleanBase.Core.ViewModels.Response;
using CleanBase.Core.ViewModels.Response.Generic;
using Core.Identity.Interfaces;
using Core.ViewModels.Requests.Identity;
using Core.ViewModels.Requests.User;
using Core.ViewModels.Responses.Identity;
using Core.ViewModels.Responses.User;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net;


namespace APis.Controllers;
[ApiController]
[Route("api/[controller]")]
[ApiVersion("1.0")]
[Authorize(
        AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme
    )]
public class IdentityController : ApiControllerBase
{
    private readonly IIdentityService _identityService;

    public IdentityController(ICoreProvider coreProvider, IIdentityService identityService) : base(coreProvider)
    {
        _identityService = identityService;
    }

    [AllowAnonymous]
    [HttpPost("register")]
    [ProducesResponseType(typeof(ActionResponse<UserResponse>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(FailActionResponse), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> CreateUserAsnyc(
        [FromBody] CreateUserRequest request,
        CancellationToken cancellationToken = default)
    {
        var result = await _identityService.CreateUserAsync(request);



        return CreateSuccessResult(result);
    }

    [AllowAnonymous]
    [HttpPost("login")]
    [ProducesResponseType(typeof(ActionResponse<LoginResponse>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(FailActionResponse), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> LoginAsync(
    [FromBody] LoginRequest request,
    CancellationToken cancellationToken = default)
    {
        var result = await _identityService.LoginAsync(request);

        return CreateSuccessResult(result);
    }

    [Authorize]
    [HttpGet("check")]
    [ProducesResponseType(typeof(ActionResponse<bool>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(FailActionResponse), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> CheckAsync(
    CancellationToken cancellationToken = default)
    {
        var stopwatch = Stopwatch.StartNew();
        var result = true;
        stopwatch.Stop();
        this.Logger.Error(($"CheckAsync executed in {stopwatch.ElapsedMilliseconds} ms"));
        return Ok(result);
    }
}
