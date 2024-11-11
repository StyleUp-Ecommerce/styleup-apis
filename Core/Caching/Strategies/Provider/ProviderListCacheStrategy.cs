using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Caching.Strategies.Provider
{
    internal class ProviderListCacheStrategy : ICacheStrategy<Guid>
    {
        public Task<string> GetKeyAsync(Guid id) => Task.FromResult($"Provider_{id}");
    }
}
