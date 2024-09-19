using CleanBase.Core.Entities;
using Core.ViewModels.Responses.CartItem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModels.Responses.Cart
{
    public class GetCartResponse
    {
        public ICollection<CartItemResponse>? Items { get; set; }
        public int TotalPrice { get; set; }
    }
}
