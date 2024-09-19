using CleanBase.Core.Entities;
using CleanBase.Core.ViewModels.Request.Base;
using Core.ViewModels.Requests.CartItem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModels.Requests.Cart
{
    public class CartRequest : EntityRequestBase
    {
        public Guid UserId { get; set; }
        public ICollection<CartItemRequest> CartItems { get; set; }
    }
}
