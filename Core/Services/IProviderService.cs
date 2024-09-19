using CleanBase.Core.Services.Core.Generic;
using Core.Entities;
using Core.ViewModels.Requests.Provider;
using Core.ViewModels.Responses.Provider;

namespace Core.Services
{
    public interface IProviderService : IServiceBase<Provider, ProviderRequest, ProviderResponse, GetAllProviderRequest>
    {
    }
}