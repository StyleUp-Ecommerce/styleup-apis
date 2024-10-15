using CleanBase.Core.Data.UnitOfWorks;
using CleanBase.Core.Domain.Domain.Services.GenericBase;
using CleanBase.Core.Services.Core.Base;
using Core.Entities;
using Core.Services;
using Core.ViewModels.Requests.Provider;
using Core.ViewModels.Responses.Provider;

namespace Domain.Services
{
    public class ProviderService : ServiceBase<Provider, ProviderRequest, ProviderResponse, GetAllProviderRequest>, IProviderService
    {
        public ProviderService(ICoreProvider coreProvider, IUnitOfWork unitOfWork) : base(coreProvider, unitOfWork)
        {
        }

        public async Task<string> ColorsSupport(Guid id)
        {
            var colors = this.Repository
                    .Where(r => r.Id == id)
                    .Select(r => r.Colors)
                    .FirstOrDefault();

            return colors;
        }


        public async Task<string> SizesSupport(Guid id)
        {
            var sizes = this.Repository
                    .Where(r => r.Id == id)
                    .Select(r => r.Sizes)
                    .FirstOrDefault();

            return sizes;
        }
    }
}
