using CleanBase.Core.Domain.Generic;
using CleanBase.Core.ViewModels.Profiles;
using Core.Entities;
using Core.ViewModels.Requests.ProviderRate;
using Core.ViewModels.Responses.ProviderRate;

namespace Core.ViewModels.Profiles
{
    public class ProviderRateProfile : ProfileBase
    {
        protected override void DefaultMapping()
        {
            CreateMap<ProviderRateRequest, ProviderRate>();
            CreateMap<ProviderRate, ProviderRateResponse>()
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.Author));
            CreateMap<ListResult<ProviderRate>, ListResult<ProviderRateResponse>>();

        }
    }
}
