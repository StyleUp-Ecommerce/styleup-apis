using CleanBase.Core.ViewModels.Response.Base;

namespace Core.ViewModels.Responses.OrderItem
{
    public class OrderItemResponse : EntityAuditResponseBase
    {
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public Guid OrderId { get; set; }
        public Guid CustomCanvasId { get; set; }
    }
}
