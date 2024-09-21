using CleanBase.Core.ViewModels.Response.Base;
using Core.ViewModels.Responses.CartItem;

namespace Core.ViewModels.Responses.Cart
{
    public class CartResponse : EntityAuditResponseBase
    {
        public ICollection<CartItemResponse> Items { get; set; }
        public int TotalPrice { get; set; }
    }
}
