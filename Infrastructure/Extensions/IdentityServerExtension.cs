using Core.Entities;
using Core.Identity.Constants;
using Infrastructure.Commons;
using Infrastructure.IdentityServer.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions
{
    public static class IdentityServerExtension
    {
        public static IServiceCollection AddIdentityServerExtension(
            this IServiceCollection services,
            IConfiguration configuration
        )
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            var migrationAssembly = typeof(IdentityServerExtension).Assembly.FullName;
            var settings = configuration.GetSection(nameof(AppConfig)).Get<AppConfig>();

            services
                .AddIdentityServer(options =>
                {
                    options.Events.RaiseErrorEvents = true;
                    options.Events.RaiseInformationEvents = true;
                    options.Events.RaiseFailureEvents = true;
                    options.Events.RaiseSuccessEvents = true;

                    options.Authentication.CookieLifetime = TimeSpan.FromDays(30);
                    options.Authentication.CookieSlidingExpiration = true;
                    options.IssuerUri = settings.IdentityServerConfig.IssuerUri;

                    options.UserInteraction.LoginUrl = IdentityDefaults.LoginUrl;
                    options.UserInteraction.LogoutUrl = IdentityDefaults.LogoutUrl;
                })
                .AddConfigurationStore(options =>
                {
                    options.ConfigureDbContext = context =>
                        context.UseNpgsql(
                            connectionString,
                            sql => sql.MigrationsAssembly(migrationAssembly)
                        );
                })
                .AddOperationalStore(options =>
                {
                    options.ConfigureDbContext = context =>
                        context.UseNpgsql(
                            connectionString,
                            sql => sql.MigrationsAssembly(migrationAssembly)
                        );

                    options.EnableTokenCleanup = true;
                })
                .AddProfileService<ProfileService>()
                .AddAspNetIdentity<User>();

            return services;
        }
    }
}
