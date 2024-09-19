using CleanBase.Core.Services.Core.Generic;
using Core.Entities;
using Core.ViewModels.Requests.ProviderRate;
using Core.ViewModels.Responses.ProviderRate;

namespace Core.Services
{
    public interface IProviderRateService : IServiceBase<ProviderRate, ProviderRateRequest, ProviderRateResponse, GetAllProviderRateRequest>
    {
    }
}