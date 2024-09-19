using CleanBase.Core.ViewModels.Response.Base;
using Core.ViewModels.Responses.Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModels.Responses.TemplateCanvas
{
    public class TemplateCanvasResponse : EntityAuditNameResponseBase
    {
        public string Descriptions { get; set; }
        public string Image { get; set; }

        public string Content { get; set; }

        public ProviderResponse Provider { get; set; }
    }
}
