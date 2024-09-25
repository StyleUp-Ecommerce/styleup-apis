using CleanBase.Core.ViewModels.Request.Base;

namespace Core.ViewModels.Requests.OrderItem
{
    public class OrderItemRequest : EntityRequestBase
    {
        public int Quantity { get; set; }
        public Guid CartItemId { get; set; }
        public Guid CustomCanvasId { get; set; }
        public string Size { get; set; }
    }
}
