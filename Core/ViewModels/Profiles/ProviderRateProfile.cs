using CleanBase.Core.ViewModels.Profiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Core.Entities;
using Core.ViewModels.Requests.ProviderRate;
using Core.ViewModels.Responses.ProviderRate;
using CleanBase.Core.Domain.Generic;

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
