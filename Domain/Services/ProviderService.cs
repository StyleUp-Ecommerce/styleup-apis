using CleanBase.Core.Data.UnitOfWorks;
using CleanBase.Core.Domain.Domain.Services.GenericBase;
using CleanBase.Core.Domain.Exceptions;
using CleanBase.Core.Services.Core.Base;
using Core.Caching;
using Core.Caching.Strategies;
using Core.Caching.Strategies.Provider;
using Core.Entities;
using Core.Services;
using Core.ViewModels.Requests.Provider;
using Core.ViewModels.Responses.Provider;

namespace Domain.Services
{
    public class ProviderService : ServiceBase<Provider, ProviderRequest, ProviderResponse, GetAllProviderRequest>, IProviderService
    {
        private readonly ICacheProvider _cacheProvider;
        public ProviderService(ICoreProvider coreProvider, IUnitOfWork unitOfWork, ICacheProvider cacheProvider) : base(coreProvider, unitOfWork)
        {
            _cacheProvider = cacheProvider;
        }

        public async Task<string> ColorsSupport(Guid id)
        {
            var colors = this.Repository
             .Where(r => r.Id == id)
             .Select(r => r.Colors)
             .FirstOrDefault();

            if (colors is null) return "Nothing color support";
            return colors;
        }


        public async Task<string> SizesSupport(Guid id)
        {
            var sizes = this.Repository
             .Where(r => r.Id == id)
             .Select(r => r.Sizes)
             .FirstOrDefault();

            if (sizes is null) return "Nothing size support";
            return sizes;

        }
    }
}
