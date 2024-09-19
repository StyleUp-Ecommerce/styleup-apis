using CleanBase.Core.ViewModels.Response.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModels.Responses.CustomCanvas
{
    public class GetCustomCanvasResponse : EntityAuditNameResponseBase
    {
        public string Content { get; set; }
        public string ImageUrl { get; set; }
        public string? LensVRUrl { get; set; }
        public bool IsPublic { get; set; } = false;
        public decimal Price { get; set; }
        public string ColorString { get; set; }
        public Guid AuthorId { get; set; }
        public Guid TemplateId { get; set; }

    }
}
