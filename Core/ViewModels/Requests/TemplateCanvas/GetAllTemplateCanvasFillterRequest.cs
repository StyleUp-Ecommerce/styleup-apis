using CleanBase.Core.ViewModels.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Core.ViewModels.Requests.TemplateCanvas
{
    public class GetAllTemplateCanvasFillterRequest : GetAllTemplateCanvasRequest
    {
        public TemplateCanvasFilter? Filter { get; set; } 

    }

    public class TemplateCanvasFilter
    {
        public PriceRange PriceRange { get; set; }
        public string Color { get; set; }
        public string Sizes { get; set; }
    }
    public class PriceRange
    {
        public decimal MinPrice { get; set; }
        public decimal MaxPrice { get; set; }
    }
}

