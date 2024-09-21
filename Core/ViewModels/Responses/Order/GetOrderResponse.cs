using Core.ViewModels.Responses.OrderItem;

namespace Core.ViewModels.Responses.Order
{
    public class GetOrderResponse
    {
        public string Address { get; set; }
        public string RecipientPhone { get; set; }
        public string RecipientName { get; set; }

        public Guid AuthorId { get; set; }
        public string StatusString { get; set; }

        public virtual ICollection<OrderItemResponse> OrderItems { get; set; }
    }
}
