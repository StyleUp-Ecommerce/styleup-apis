using CleanBase.Core.Validators.Generic;
using Microsoft.Extensions.DependencyInjection;

namespace Domain.Validators
{
    public static class ValidatorBootstrapper
    {
        public static IServiceCollection RegisterValidators(this IServiceCollection services)
        {
            return services;
        }
    }
}
