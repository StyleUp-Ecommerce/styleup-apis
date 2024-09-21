using Core.ViewModels.Responses.CartItem;

namespace Core.ViewModels.Responses.Cart
{
    public class GetCartResponse
    {
        public ICollection<CartItemResponse>? Items { get; set; }
        public int TotalPrice { get; set; }
    }
}
