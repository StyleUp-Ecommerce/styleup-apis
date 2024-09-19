using CleanBase.Core.Validators.Generic;
using Core.ViewModels.Requests.Pet;
using Core.ViewModels.Requests.User;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
