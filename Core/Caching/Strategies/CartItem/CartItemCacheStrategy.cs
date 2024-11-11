﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Caching.Strategies.CartItem
{
    public class CartItemCacheStrategy : ICacheStrategy<Guid>
    {
        public Task<string> GetKeyAsync(Guid id) => Task.FromResult($"CartItem_{id}");
    }
}