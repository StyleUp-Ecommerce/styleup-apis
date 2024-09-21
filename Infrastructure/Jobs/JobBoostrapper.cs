using CleanBase.Core.Services.Jobs;
using Microsoft.Extensions.DependencyInjection;
namespace Infrastructure.Jobs
{
    public static class JobBoostrapper
    {
        public static IServiceCollection RegisterJobConsumers(this IServiceCollection services)
        {
            services.AddSingleton<IProcessingJobConsumer, JobRegisterService>();
            return services;
        }
    }
}
