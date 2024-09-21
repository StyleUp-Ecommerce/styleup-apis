using CleanBase.Core.Domain.Generic;
using CleanBase.Core.ViewModels.Profiles;
using Core.Entities;
using Core.ViewModels.Requests.Provider;
using Core.ViewModels.Responses.Provider;

namespace Core.ViewModels.Profiles
{
    public class ProviderProfile : ProfileBase
    {
        protected override void DefaultMapping()
        {
            CreateMap<ProviderRequest, Provider>();
            CreateMap<Provider, ProviderResponse>();
            CreateMap<ListResult<Provider>, ListResult<ProviderResponse>>();

        }
    }
}
