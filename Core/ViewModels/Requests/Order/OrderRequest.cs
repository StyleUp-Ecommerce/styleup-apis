using CleanBase.Core.ViewModels.Request.Base;
using Core.ViewModels.Requests.CartItem;
using Core.ViewModels.Requests.OrderItem;

namespace Core.ViewModels.Requests.Order
{
    public class OrderRequest : EntityRequestBase
    {
        public List<OrderItemRequest> Items { get; set; }
        public string Address { get; set; }
        public string RecipientPhone { get; set; }
        public string RecipientEmail { get; set; }
        public string RecipientName { get; set; }
        public string VoucherCode { get; set; }

    }
}
