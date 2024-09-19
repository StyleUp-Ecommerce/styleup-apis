using CleanBase.Core.Entities;
using CleanBase.Core.ViewModels.Request.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModels.Requests.CartItem
{
    public class CartItemRequest : EntityRequestBase
    {
        public int Quantity { get; set; }
        public int CustomCanvasId { get; set; }
    }
}
