using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModels.Requests.Cart
{
    public class AddToCartRequest
    {
        public int Quantity { get; set; }
        public Guid CustomCanvasId { get; set; }
        public string Size { get; set; }
    }
}
