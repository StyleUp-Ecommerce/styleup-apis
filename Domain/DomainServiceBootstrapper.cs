using Core.Identity.Email.Interfaces;
using Core.Identity.Interfaces;
using Core.Services;
using Domain.Identity.Email;
using Domain.Identity.Providers;
using Domain.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public static class DomainServiceBootstrapper
    {
        public static IServiceCollection RegisterDomainService(this IServiceCollection services)
        {
            services.AddScoped<IPetService, PetService>();

            services.AddSingleton(_ =>
            {
                return new List<IEmailClassifier>()
                {
                        new EmailVerificationTemplate(),
                        new ResetPasswordTemplate(),
                };

            });
            services.AddSingleton(_ =>
            {
                return new List<IExternalProvider>() { new GoogleProvider() };
            });
            return services;
        }
    }
}