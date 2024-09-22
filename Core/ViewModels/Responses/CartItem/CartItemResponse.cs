using Core.ViewModels.Responses.CustomCanvas;

namespace Core.ViewModels.Responses.CartItem
{
    public class CartItemResponse
    {
        public int Quantity { get; set; }
        public string Size { get; set; }
        public GetCanvasInfoCartResponse CustomCanvas { get; set; }
    }
}
