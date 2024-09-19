using Infrastructure.Context;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using CleanBase.Core.Infrastructure.Jobs;
using Infrastructure.Jobs;
using CleanBase.Core.Infrastructure.Services;
using Infrastructure.Commons;
using Microsoft.Extensions.Configuration;
using Core.Identity.Interfaces;
using Core.Identity.Email.Interfaces;
using Infrastructure.Identity.Services;
using Infrastructure.Email;
using Infrastructure.Extensions;

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
            services.AddScoped<IEmailService, EmailService>();

            services.RegisterJobConsumers();

			return services;
		}
	}
}
