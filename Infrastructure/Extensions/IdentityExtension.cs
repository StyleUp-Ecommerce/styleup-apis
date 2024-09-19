using Core.Entities;
using Core.Identity.Constants.Authorization;
using Core.IdentityServer.Constants.Authorization;
using Duende.IdentityServer;
using IdentityModel;
using Infrastructure.Context;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CleanBase.Core.Services.Core.Base;
using CleanBase.Core.Security;
using Infrastructure.Commons;

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

            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.Audience = ApiResource.Template;
                    options.Authority = settings.IdentityServerConfig.Authority;

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateLifetime = true, // Xác thực thời hạn của token
                        ClockSkew = TimeSpan.Zero, // Không có độ lệch thời gian để tránh chấp nhận token hết hạn

                        // Key dùng để xác thực token phải khớp với key khi mã hóa
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.JwtConfig.SigningKey)),

                        ValidateIssuer = true, // Xác thực `Issuer` của token
                        ValidIssuer = settings.JwtConfig.Issuer, // Phải khớp với `Issuer` trong token

                        ValidateAudience = !string.IsNullOrEmpty(settings.JwtConfig.Audience), // Xác thực Audience nếu có
                        ValidAudience = settings.JwtConfig.Audience, // `Audience` phải khớp với trong token

                        ValidateIssuerSigningKey = true, // Xác thực rằng token đã được ký
                    };
                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
   {
       // Lấy token từ request
       var token = context.Token;

       // Log token ra console hoặc dùng logger
       Console.WriteLine($"Received Token: {token}");
       var logger = context.HttpContext.RequestServices.GetService<ISmartLogger>();
       logger?.Error($"Token Received: {token}");

       return Task.CompletedTask;
   },
                        OnTokenValidated = async context =>
                        {
                            
                            var userName = context.Principal?.Identity?.Name;
                            var logger = context.HttpContext.RequestServices.GetService<ISmartLogger>();
                            logger.UserName = string.IsNullOrEmpty(userName) ? "anonymous" : userName;
                        }
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
                options.AddPolicy(Policy.ReadAccess, policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim(JwtClaimTypes.Scope, ApiScope.Read);
                });

                options.AddPolicy(Policy.WriteAccess, policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim(JwtClaimTypes.Scope, ApiScope.Write);
                });

                options.AddPolicy(Policy.UpdateAccess, policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim(JwtClaimTypes.Scope, ApiScope.Update);
                });

                options.AddPolicy(Policy.DeleteAccess, policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim(JwtClaimTypes.Scope, ApiScope.Delete);
                });

                options.AddPolicy(Policy.UpdateProfilePasswordAccess, policy =>
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

            services.Configure<DataProtectionTokenProviderOptions>(options =>
            {
                options.TokenLifespan = TimeSpan.FromDays(2);
            });

            return services;
        }

    }
}
