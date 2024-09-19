using CleanBase.Core.Domain.Generic;
using CleanBase.Core.ViewModels.Profiles;
using Core.Entities;
using Core.ViewModels.Requests.Pet;
using Core.ViewModels.Requests.User;
using Core.ViewModels.Responses.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModels.Profiles
{
    public partial class PetProfile : ProfileBase
	{
		protected override void DefaultMapping()
		{
			CreateMap<PetRequest, Pet>();
			CreateMap<Pet, UserResponse>();
			CreateMap<PetRequest, UserResponse>();
			CreateMap<ListResult<Pet>, ListResult<UserResponse>>();
		}
	}
}
