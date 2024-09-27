using CleanBase.Core.Infrastructure.Jobs;
using Core.Identity.Email.Interfaces;
using Core.Identity.Interfaces;
using Infrastructure.Commons;
using Infrastructure.Email;
using Infrastructure.Identity.Services;
using Infrastructure.Jobs;
using Infrastructure.Repositories.DI;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class InfrastructureBootstrapper
    {
        public static IServiceCollection RegisterInfrastructureService(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            JobBootStrapper.UseHangfireBackgroundJob(services);

            services.Configure<AppConfig>(configuration.GetSection(nameof(AppConfig)));
            services.AddScoped<IIdentityService, IdentityService>();
            services.AddScoped<JwtService>();
            services.AddScoped<IEmailService, EmailService>();

            services.RegisterJobConsumers();

            return services;
        }
    }
}
