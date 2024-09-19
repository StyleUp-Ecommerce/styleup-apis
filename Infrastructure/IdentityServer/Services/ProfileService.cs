using Core.Entities;
using Duende.IdentityServer.Extensions;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.IdentityServer.Services
{
    public sealed class ProfileService : IProfileService
    {
        private readonly ILogger<ProfileService> _logger;
        private readonly UserManager<User> _userManager;

        public ProfileService(ILogger<ProfileService> logger, UserManager<User> userManager)
        {
            _logger = logger;
            _userManager = userManager;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            try
            {
                var sub = context.Subject.GetSubjectId();
                var user = await _userManager.FindByIdAsync(sub).ConfigureAwait(false);

                var claims = await _userManager.GetClaimsAsync(user).ConfigureAwait(false);

                context.AddRequestedClaims(claims);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, nameof(GetProfileDataAsync));
                throw;
            }
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            try
            {
                var sub = context.Subject.GetSubjectId();
                var user = await _userManager.FindByIdAsync(sub);

                context.IsActive = user is not null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, nameof(IsActiveAsync));
                throw;
            }
        }
    }

}
