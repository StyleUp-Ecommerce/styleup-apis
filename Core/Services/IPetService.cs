using CleanBase.Core.Services.Core.Generic;
using Core.Entities;
using Core.ViewModels.Requests.Pet;
using Core.ViewModels.Responses.User;

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
