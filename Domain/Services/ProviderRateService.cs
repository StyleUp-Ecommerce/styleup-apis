using CleanBase.Core.Data.UnitOfWorks;
using CleanBase.Core.Domain.Domain.Services.GenericBase;
using CleanBase.Core.Services.Core.Base;
using Core.Entities;
using Core.Services;
using Core.ViewModels.Requests.ProviderRate;
using Core.ViewModels.Responses.ProviderRate;

namespace Domain.Services
{
    internal class ProviderRateService : ServiceBase<ProviderRate, ProviderRateRequest, ProviderRateResponse, GetAllProviderRateRequest>, IProviderRateService
    {
        public ProviderRateService(ICoreProvider coreProvider, IUnitOfWork unitOfWork) : base(coreProvider, unitOfWork)
        {
        }
    }
}
