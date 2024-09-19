using CleanBase.Core.Services.Jobs;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
