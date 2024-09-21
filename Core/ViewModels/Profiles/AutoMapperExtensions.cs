using Microsoft.Extensions.DependencyInjection;

namespace Core.ViewModels.Profiles
{
    public static class AutoMapperExtensions
    {
        public static IServiceCollection AddAutoMapperProfiles(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(AutoMapperExtensions).Assembly);
            return services;
        }
    }
}
