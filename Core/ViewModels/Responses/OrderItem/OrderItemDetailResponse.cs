using Core.ViewModels.Responses.CustomCanvas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModels.Responses.OrderItem
{
    public class OrderItemDetailResponse
    {
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }
        public string ImageUrl { get; set; }
        public string CustomCanvasId { get; set; }
    }
}
