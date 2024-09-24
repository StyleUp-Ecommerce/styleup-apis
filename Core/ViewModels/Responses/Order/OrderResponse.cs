using CleanBase.Core.Entities;
using Core.ViewModels.Responses.OrderItem;

namespace Core.ViewModels.Responses.Order
{
    public class OrderResponse : EntityAuditActive
    {
        public string OrderStatus { get; set; }
        public string OrderCode { get; set; }
        public List<OrderItemDetailResponse> Items { get; set; }
        public decimal TotalPrice { get; set; }
        public string DiscountType { get; set; }

        public decimal DiscountValue { get; set; }

    }
}
