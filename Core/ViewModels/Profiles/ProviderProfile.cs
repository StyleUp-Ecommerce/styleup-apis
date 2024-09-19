using CleanBase.Core.ViewModels.Profiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Core.Entities;
using Core.ViewModels.Requests.Provider;
using Core.ViewModels.Responses.Provider;
using CleanBase.Core.Domain.Generic;

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
