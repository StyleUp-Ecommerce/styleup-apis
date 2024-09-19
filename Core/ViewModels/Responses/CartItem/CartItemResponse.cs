using Core.ViewModels.Responses.CustomCanvas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModels.Responses.CartItem
{
    public class CartItemResponse
    {
        public int Quantity { get; set; }
        public GetCanvasInfoCartResponse CustomCanvas { get; set; }
    }
}
