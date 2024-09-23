using CleanBase.Core.ViewModels.Response.Base;
using Core.ViewModels.Responses.CustomCanvas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModels.Responses.TemplateCanvas
{
    public class GetTemplateCanvasProductResponse
    {
        public List<CustomCanvasProductResponse> Products { get; set; }
        public List<string> Images { get; set; }
        public string Colors {  get; set; }
        public string Name { get; set; }

    }
}
