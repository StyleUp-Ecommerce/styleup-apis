using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Caching.Strategies.Cart
{
    public class CartCacheStrategy : ICacheStrategy<Guid>
    {
        public Task<string> GetKeyAsync(Guid authroId) => Task.FromResult($"Cart_{authroId}");
    }
}
