using CleanBase.Core.ViewModels.Request.Base;

namespace Core.ViewModels.Requests.CartItem
{
    public class CartItemRequest : EntityRequestBase
    {
        public int Quantity { get; set; }
        public int CustomCanvasId { get; set; }
    }
}
