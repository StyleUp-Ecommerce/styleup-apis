using Core.Entities;
using Core.Identity.Constants.Authorization;
using Core.IdentityServer.Constants.Authorization;
using Duende.IdentityServer;
using IdentityModel;
using Infrastructure.Commons;
using Infrastructure.Context;
using Infrastructure.Repositories.DI;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Infrastructure.Extensions
{
    public static class IdentityExtension
    {
        public static IServiceCollection AddIdentityExtension(
            this IServiceCollection services,
            IConfiguration configuration
        )
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            var settings = configuration.GetSection(nameof(AppConfig)).Get<AppConfig>();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                //options.RequireHttpsMetadata = false;
                //options.Authority = settings.IdentityServerConfig.Authority;

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateLifetime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.JwtConfig.SigningKey)),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidAudience = settings.JwtConfig.Audience,
                    ValidateIssuerSigningKey = true ,
                };
            })
            .AddGoogle(options =>
            {
                var google = settings.IdentityServerConfig.Clients.GoogleWeb;
                options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
                options.ClientId = google.ExternalClientId;
                options.ClientSecret = google.ExternalClientSecret;
            });

            JwtExtensions.Configure(configuration);

            services.AddAuthorization(options =>
            {
                options.AddPolicy(ApiPolicy.ReadAccess, policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim(JwtClaimTypes.Scope, ApiScope.Read);
                });

                options.AddPolicy(ApiPolicy.WriteAccess, policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim(JwtClaimTypes.Scope, ApiScope.Write);
                });

                options.AddPolicy(ApiPolicy.UpdateAccess, policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim(JwtClaimTypes.Scope, ApiScope.Update);
                });

                options.AddPolicy(ApiPolicy.DeleteAccess, policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim(JwtClaimTypes.Scope, ApiScope.Delete);
                });

                options.AddPolicy(ApiPolicy.UpdateProfilePasswordAccess, policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim(JwtClaimTypes.Scope, ApiScope.Update);
                    policy.RequireClaim(JwtClaimTypes.Scope, ApiScope.UpdateProfilePassword);
                });

                options.DefaultPolicy = new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser()
                    .Build();
            });

            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseNpgsql(connectionString);
            });

            services
                .AddIdentity<User, IdentityRole<Guid>>(options =>
                {
                    options.User.RequireUniqueEmail = true;
                    options.SignIn.RequireConfirmedEmail = true;
                    options.Password.RequiredLength = 8;
                    options.Password.RequireLowercase = true;
                    options.Password.RequireUppercase = true;
                    options.Password.RequireDigit = true;
                })
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            services.RegisterRepositories();
            services.Configure<DataProtectionTokenProviderOptions>(options =>
            {
                options.TokenLifespan = TimeSpan.FromDays(2);
            });

            return services;
        }

    }
}
