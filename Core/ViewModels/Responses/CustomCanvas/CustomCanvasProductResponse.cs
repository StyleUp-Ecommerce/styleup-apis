using CleanBase.Core.ViewModels.Response.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModels.Responses.CustomCanvas
{
    public class CustomCanvasProductResponse : EntityAuditResponseBase
    {
        public List<string> Images { get; set; }
        public string? LensVRUrl { get; set; }
        public decimal Price { get; set; }
        public string Color { get; set; }
        public string Sizes { get; set; }
        public Guid AuthorId { get; set; }
    }
}
