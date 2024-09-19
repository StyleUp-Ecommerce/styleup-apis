using CleanBase.Core.Services.Core.Generic;
using Core.Entities;
using Core.ViewModels.Requests.Pet;
using Core.ViewModels.Requests.User;
using Core.ViewModels.Responses.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public interface
        IPetService : IServiceBase<Pet, PetRequest, UserResponse, PetGetAllRequest>
    {
        public Task<UserResponse> GetUserRandomMessage(Guid id);

        public Task<bool> IsValidHuman(PetRequest userRequest);
        Task TriggerUpdateUserAge();
    }
}
