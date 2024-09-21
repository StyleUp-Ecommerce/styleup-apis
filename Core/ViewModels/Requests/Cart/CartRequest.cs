using CleanBase.Core.ViewModels.Request.Base;
using Core.ViewModels.Requests.CartItem;

namespace Core.ViewModels.Requests.Cart
{
    public class CartRequest : EntityRequestBase
    {
        public Guid UserId { get; set; }
        public ICollection<CartItemRequest> CartItems { get; set; }
    }
}
