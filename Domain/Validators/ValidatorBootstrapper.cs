using CleanBase.Core.Validators.Generic;
using Core.ViewModels.Requests.Pet;
using Microsoft.Extensions.DependencyInjection;

namespace Domain.Validators
{
    public static class ValidatorBootstrapper
    {
        public static IServiceCollection RegisterValidators(this IServiceCollection services)
        {
            services.AddScoped<IValidator<PetRequest>, PetValidator>();

            return services;
        }
    }
}
