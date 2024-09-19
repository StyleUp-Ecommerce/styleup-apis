using CleanBase.Core.Data.UnitOfWorks;
using CleanBase.Core.Domain.Domain.Services.GenericBase;
using CleanBase.Core.Domain.Exceptions;
using CleanBase.Core.Security;
using CleanBase.Core.Services.Core.Base;
using CleanBase.Core.Validators.Generic;
using Core.Entities;
using Core.Helpers;
using Core.Services;
using Core.ViewModels.Requests.Pet;
using Core.ViewModels.Requests.User;
using Core.ViewModels.Responses.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Abstractions;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class PetService : ServiceBase<Pet, PetRequest, UserResponse, PetGetAllRequest>, IPetService
	{
		private readonly IValidator<PetRequest> _validator;
		public PetService(ICoreProvider coreProvider, IUnitOfWork unitOfWork) : base(coreProvider, unitOfWork)
		{
		}

		public async Task<UserResponse> GetUserRandomMessage(Guid id)
		{

			var user = await this.Repository.FirstOrDefaultAsync(u => u.Id == id);

			var newUser = new Pet()
			{
				Name = "HE",
				Age = 20
			};

			this.Repository.Add(newUser);
			UnitOfWork.SaveChanges();
			var errorDetails = new List<ErrorCodeDetail>();

			if (true)
			{
				errorDetails.Add(new ErrorCodeDetail { Message = "First name is required." });
			}

			if (true)
			{
				errorDetails.Add(new ErrorCodeDetail { Message = "Last name is required." });
			}

			if (errorDetails.Any())
			{
				throw new DomainException("Validation failed.", "EER_1", null, 500, errorDetails);
			}

			throw new Exception("error", new Exception("err"));

			return null;
		}

		public async Task<bool> IsValidHuman(PetRequest userRequest)
		{
			return _validator.Validate(userRequest).IsValid;
		}

		public async Task TriggerUpdateUserAge()
		{
			//var users = await this.Repository.GetAllAsync();

			//foreach (var user in users)
			//{
			//	user.Age++;
			//}

			//var userslist = await users.ToListAsync();
			//Repository.BatchUpdate(userslist,true);
				
		}
	}
}
