using CleanBase.Core.Api.Controllers;
using CleanBase.Core.Services.Core.Base;
using CleanBase.Core.ViewModels.Response;
using CleanBase.Core.ViewModels.Response.Generic;
using Core.Identity.Constants.Authorization;
using Core.Identity.Interfaces;
using Core.ViewModels.Requests.Identity;
using Core.ViewModels.Requests.User;
using Core.ViewModels.Responses.Identity;
using Core.ViewModels.Responses.User;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpPost("test-auth")]
    [ProducesResponseType(typeof(ActionResponse<LoginResponse>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(FailActionResponse), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> TestAuth(
    CancellationToken cancellationToken = default
)
    {
        return Ok(true);
    }
    //[AllowAnonymous]
    //[HttpPost("verify")]
    //public async Task<ActionResult<Result<object, object>>> VerifyEmailAsnyc(
    //    [FromBody] VerifyEmailCommand request,
    //    CancellationToken cancellationToken = default)
    //{
    //    var result = await _identityService.VerifyEmailAsync(request, cancellationToken);

    //    if (!result.Succeeded)
    //        return BadRequest(result);

    //    return Ok(result);
    //}

    //[AllowAnonymous]
    //[HttpPost("verify/resend")]
    //public async Task<ActionResult<Result<object, object>>> ResendVerificationEmailAsnyc(
    //    [FromBody] ResendConfirmationEmailCommand request,
    //    CancellationToken cancellationToken = default)
    //{
    //    var result = await _identityService.ResendVerificationEmailAsync(request, cancellationToken);

    //    if (!result.Succeeded)
    //        return BadRequest(result);

    //    return Ok(result);
    //}

    //[AllowAnonymous]
    //[HttpPost("password/forgot")]
    //public async Task<ActionResult<Result<object, object>>> ForgotPasswordAsync(
    //    [FromBody] ForgotPasswordCommand request,
    //    CancellationToken cancellationToken = default)
    //{
    //    var result = await _identityService.ForgotPasswordAsync(request, cancellationToken);

    //    if (!result.Succeeded)
    //        return BadRequest(result);

    //    return Ok(result);
    //}

    //[AllowAnonymous]
    //[HttpPost("password/reset")]
    //public async Task<ActionResult<Result<object, object>>> ResetPasswordAsync(
    //    [FromBody] ResetPasswordCommand request,
    //    CancellationToken cancellationToken = default)
    //{
    //    var result = await _identityService.ResetPasswordAsync(request, cancellationToken);

    //    if (!result.Succeeded)
    //        return BadRequest(result);

    //    return Ok(result);
    //}

    //[Authorize(
    //    AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
    //    Policy = Policy.UpdateProfilePasswordAccess
    //)]
    //[HttpPut("password/change")]
    //public async Task<ActionResult<Result<object, object>>> ChangePasswordAsync(
    //    [FromBody] ChangePasswordCommand request,
    //    CancellationToken cancellationToken = default)
    //{
    //    request.Email = User.FindFirst(ClaimTypes.Email)?.Value;
    //    var result = await _identityService.ChangePasswordAsync(request, cancellationToken);

    //    if (!result.Succeeded)
    //        return BadRequest(result);

    //    return Ok(result);
    //}

    //[Authorize(
    //    AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
    //    Policy = Policy.DeleteAccess
    //)]
    //[HttpDelete("delete")]
    //public async Task<ActionResult<Result<object, object>>> DeleteUserAsync(
    //    CancellationToken cancellationToken = default)
    //{
    //    var email = User.FindFirst(ClaimTypes.Email)?.Value;
    //    var result = await _identityService.DeleteUserAsync(email, cancellationToken);

    //    if (!result.Succeeded)
    //        return BadRequest(result);

    //    return Ok(result);
    //}

    //[AllowAnonymous]
    //[HttpGet(IdentityDefaults.LoginPath)]
    //public async Task<IActionResult> Login(string returnUrl, CancellationToken cancellationToken = default)
    //{
    //    var result = await _identityService.GetProviderAsync(returnUrl, Request, cancellationToken);

    //    if (!result.Succeeded)
    //        return BadRequest(result);

    //    return Challenge(result.Data.Properties, result.Data.Provider);
    //}

    //[AllowAnonymous]
    //[HttpGet(IdentityDefaults.CallbackPath)]
    //public async Task<IActionResult> ExternalLoginCallback(CancellationToken cancellationToken = default)
    //{
    //    var result = await _identityService.ExternalSignInAsync(HttpContext, cancellationToken);

    //    if (!result.Succeeded)
    //        return BadRequest(result);

    //    return Redirect(result.Data);
    //}

    //[AllowAnonymous]
    //[HttpGet(IdentityDefaults.LogoutPath)]
    //public async Task<IActionResult> Logout(string logoutId, CancellationToken cancellationToken = default)
    //{
    //    var result = await _identityService.ExternalSignOutAsync(logoutId, cancellationToken);

    //    if (!result.Succeeded)
    //        return BadRequest(result);

    //    return Redirect(result.Data);
    //}
}