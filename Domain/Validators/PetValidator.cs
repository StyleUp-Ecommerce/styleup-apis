using CleanBase.Core.Validators.Generic;
using Core.ViewModels.Requests.Pet;
using Core.ViewModels.Requests.User;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Validators
{
	public class PetValidator : CoreValidator<PetRequest>
	{
		protected override void ApplyDefaultRules()
		{
			RuleFor(p => p.Age)
				.Must(age => age > 0 && age < 120)
				.WithMessage("Age must be greater than 0 and smaller than 120.");
			RuleFor(p => p.Name)
				.Must(name => !string.IsNullOrEmpty(name) && name != "string")
				.WithMessage("Name can't be 'string' and must not be null or empty.");

		}
	}
}
