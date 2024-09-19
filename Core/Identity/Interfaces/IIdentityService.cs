using CleanBase.Core.ViewModels.Response.Generic;
using Core.Entities;
using Core.ViewModels.Requests.Identity;
using Core.ViewModels.Requests.User;
using Core.ViewModels.Responses.Identity;
using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Identity.Interfaces
{
	public interface IIdentityService
	{
		Task<User> FindUserAsync(FindUserRequest request);
		Task<object> CreateUserAsync(CreateUserRequest request);
		Task<string> GenerateEmailVerificationTokenAsync(string email);
		Task<LoginResponse> LoginAsync(LoginRequest request);
		Task<object> VerifyEmailAsync(VerifyEmailRequest request);
		Task<string> GenerateResetPasswordTokenAsync(string email);
		Task<object> ResetPasswordAsync(ResetPasswordRequest request);
		Task<object> ChangePasswordAsync(ChangePasswordRequest request);
		Task<object> DeleteUserAsync(FindUserRequest request);
		Task<object> RegisterExternalAsync(AuthenticateResult result);
	}

}
