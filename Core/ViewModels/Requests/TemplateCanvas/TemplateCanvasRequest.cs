using CleanBase.Core.Entities;
using CleanBase.Core.ViewModels.Request.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModels.Requests.TemplateCanvas
{
    public class TemplateCanvasRequest : EntityRequestBase
    {
        public string Descriptions { get; set; }
        public string Image { get; set; }

        public string Content { get; set; }

        public Guid ProviderId { get; set; }

    }
}
