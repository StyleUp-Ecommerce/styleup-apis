﻿using CleanBase.Core.Domain.Exceptions;
using Core.Entities;
using Core.Identity.Email.Enums;
using Core.Identity.Email.Interfaces;
using Core.Identity.Interfaces;
using Core.IdentityServer.Constants.Authorization;
using Core.ViewModels.Requests.Identity;
using Core.ViewModels.Requests.User;
using Core.ViewModels.Responses.Identity;
using Domain.Identity.Extensions;
using IdentityModel;
using Infrastructure.Extensions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Identity.Services
{
    public sealed class IdentityService : IIdentityService
    {
        private readonly ILogger<IdentityService> _logger;
        private readonly UserManager<User> _userManager;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly SignInManager<User> _signInManager;
        private readonly JwtService _jwtService;
        private readonly IEmailService _emailService;

        public IdentityService(
            ILogger<IdentityService> logger,
            UserManager<User> userManager,
            IPasswordHasher<User> passwordHasher,
            SignInManager<User> signInManager,
            JwtService jwtService,
            IEmailService emailService
        )
        {
            _logger = logger;
            _userManager = userManager;
            _passwordHasher = passwordHasher;
            _signInManager = signInManager;
            _jwtService = jwtService;
            _emailService = emailService;
        }

        public async Task<User> FindUserAsync(FindUserRequest request)
        {
            try
            {
                var result = await _userManager.FindByEmailAsync(request.Email).ConfigureAwait(false);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, nameof(FindUserAsync));
                throw;
            }
        }


        public async Task<object> CreateUserAsync(CreateUserRequest request)
        {
            try
            {
                var searchResult = await FindUserAsync(new(request.Email));

                if (searchResult is not null)
                    throw new DomainException("User already exists.", "USER_EXISTS", null, 400, null);

                var user = request.ToEntity();
                user.PasswordHash = _passwordHasher.HashPassword(user, request.Password);

                var result = await _userManager.CreateAsync(user).ConfigureAwait(false);


                if (!result.Succeeded)
                {
                    var errorList = result.Errors.Select(error => new ErrorCodeDetail
                    {
                        Code = error.Code,
                        Message = error.Description
                    }).ToList();

                    throw new DomainException("Failed to create user.", "USER_CREATION_FAILED", null, 400, errorList);
                }
                var claims = user.SelectClaims();
                claims.Add(new Claim(JwtClaimTypes.Scope, ApiScope.Read));
                claims.Add(new Claim(JwtClaimTypes.Scope, ApiScope.Delete));

                await _userManager.AddClaimsAsync(user, claims).ConfigureAwait(false);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, nameof(CreateUserAsync));
                throw;
            }
        }


        public async Task<bool> GenerateEmailVerificationTokenAsync(string email)
        {
            try
            {
                var searchResult = await FindUserAsync(new(email));

                if (searchResult is null)
                    throw new DomainException("User not exits. ", "ERROR", null, 400, null);


                var token = await _userManager
                .GenerateEmailConfirmationTokenAsync(searchResult)
                .ConfigureAwait(false);

                var stringToken = Convert.ToBase64String(Encoding.UTF8.GetBytes(token));

                var parameters = _emailService.GenerateEmailConfirmationParameters(searchResult, stringToken);

                await _emailService.SendAsync(EmailType.Verification, email, parameters);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, nameof(FindUserAsync));
                throw;
            }
        }

        public async Task<object> VerifyEmailAsync(VerifyEmailRequest request)
        {
            try
            {
                var searchResult = await FindUserAsync(new(request.Email));

                if (searchResult is null)
                    throw new DomainException("User not exits. ", "ERROR", null, 500, null);

                var token = Encoding.UTF8.GetString(Convert.FromBase64String(request.Token));

                var result = await _userManager
                    .ConfirmEmailAsync(searchResult, token)
                    .ConfigureAwait(false);

                if (!result.Succeeded)
                    throw new DomainException("Cannot comfirm email. ", "ERROR", null, 500, null);

                var claimsResult = await _userManager
                    .AddClaimsAsync(searchResult, searchResult.SelectClaims())
                    .ConfigureAwait(false);

                if (!claimsResult.Succeeded)
                    throw new DomainException("Cannot comfirm email. ", "ERROR", null, 500, null);

                var tokens = GenerateTokensAsync(searchResult);
                return tokens;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, nameof(VerifyEmailAsync));
                throw;
            }
        }

        public async Task<bool> GenerateResetPasswordTokenAsync(string email)
        {
            try
            {
                var searchResult = await FindUserAsync(new(email));

                if (searchResult is null)
                    throw new DomainException("User not exits ", "ERROR", null, 400, null);

                var token = await _userManager
                    .GeneratePasswordResetTokenAsync(searchResult)
                    .ConfigureAwait(false);
                
                var stringToken = Convert.ToBase64String(Encoding.UTF8.GetBytes(token));

                var parameters = _emailService.GenerateResetPasswordParameters(searchResult, stringToken);

                await _emailService.SendAsync(EmailType.ResetPassword, email,parameters);
                
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, nameof(GenerateResetPasswordTokenAsync));
                throw;
            }
        }

        public async Task<object> ResetPasswordAsync(ResetPasswordRequest request)
        {
            try
            {
                var searchResult = await FindUserAsync(new(request.Email));

                if (searchResult is null)
                    throw new DomainException("User not exits. ", "ERROR", null, 400, null);

                var token = Encoding.UTF8.GetString(Convert.FromBase64String(request.Token));

                var result = await _userManager
                    .ResetPasswordAsync(searchResult, token, request.Password)
                    .ConfigureAwait(false);

                if (!result.Succeeded)
                    throw new DomainException("Cannot reset password. ", "ERROR", null, 500, null);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, nameof(ResetPasswordAsync));
                throw;
            }
        }

        public async Task<object> ChangePasswordAsync(ChangePasswordRequest request)
        {
            try
            {
                var searchResult = await FindUserAsync(new(request.Email));
                if (searchResult is null)
                    throw new DomainException("User not exits. ", "ERROR", null, 400, null);

                var isOldPasswordCorrect = await _userManager
                    .CheckPasswordAsync(searchResult, request.OldPassword)
                    .ConfigureAwait(false);

                if (!isOldPasswordCorrect)
                    throw new DomainException("Wrong old password. ", "ERROR", null, 400, null);

                var user = searchResult;
                user.PasswordHash = _passwordHasher.HashPassword(user, request.NewPassword);

                var result = await _userManager.UpdateAsync(user).ConfigureAwait(false);
                if (!result.Succeeded)
                    throw new DomainException("Cannot update ", "ERROR", null, 400, null);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, nameof(ChangePasswordAsync));
                throw;
            }
        }

        public async Task<object> DeleteUserAsync(FindUserRequest request)
        {
            try
            {
                var userSearchResult = await FindUserAsync(new(request.Email));

                if (userSearchResult is null)
                    throw new DomainException("User not exits. ", "ERROR", null, 400, null);

                var user = userSearchResult;
                var result = await _userManager.DeleteAsync(user).ConfigureAwait(false);
                if (!result.Succeeded)
                    throw new DomainException("Cannot delete. ", "ERROR", null, 500, null);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, nameof(DeleteUserAsync));
                throw;
            }
        }

        public async Task<object> RegisterExternalAsync(AuthenticateResult result)
        {
            try
            {
                var provider = result.SelectIdentityProvider();

                var userId = result.SelectUserId();
                var user = await _userManager.FindByLoginAsync(provider, userId).ConfigureAwait(false);

                if (user is null)
                {
                    user = result.Principal.ToEntity();
                    var userResult = await _userManager.CreateAsync(user).ConfigureAwait(false);

                    if (!userResult.Succeeded)
                        throw new DomainException("Cannot create user. ", "ERROR", null, 500, null);

                    var claimsResult = await _userManager
                        .AddClaimsAsync(user, user.SelectClaims(provider))
                        .ConfigureAwait(false);

                    if (!claimsResult.Succeeded)
                        throw new DomainException("Cannot add claims. ", "ERROR", null, 500, null);
                }

                var info = new UserLoginInfo(provider, userId, provider);

                await _userManager.AddLoginAsync(user, info);
                await _signInManager.SignInAsync(user, isPersistent: false);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, nameof(RegisterExternalAsync));
                throw;
            }
        }
        public async Task<(string accessToken, string refreshToken)> GenerateTokensAsync(User user)
        {
            try
            {
                var claims = await _userManager.GetClaimsAsync(user);

                var accessToken = _jwtService.GenerateJwtToken(user, claims);

                var refreshToken = _jwtService.GenerateRefreshToken();


                return (accessToken, refreshToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, nameof(GenerateTokensAsync));
                throw;
            }
        }

        public async Task<LoginResponse> LoginAsync(LoginRequest request)
        {
            var searchResult = await FindUserAsync(new(request.Email));

            if (searchResult == null)
                throw new DomainException("Wrong password or email.", "ERROR", null, 400, null);

            var isRightPassword = await _userManager
                .CheckPasswordAsync(searchResult, request.Password)
                .ConfigureAwait(false);

            if (!isRightPassword)
                throw new DomainException("Wrong password or email.", "ERROR", null, 400, null);

            var tokens = await GenerateTokensAsync(searchResult);

            return new LoginResponse
            {
                AccessToken = tokens.accessToken,
                RefreshToken = tokens.refreshToken,
            };
        }

    }

}
