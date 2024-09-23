using CleanBase.Core.ViewModels.Response.Base;
using Core.ViewModels.Responses.Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModels.Responses.TemplateCanvas
{
    public class GetAllTemplateCanvasResponse : EntityAuditNameResponseBase
    {
        public List<string> Images { get; set; }
        public decimal MinPrice { get; set; }
        public String Colors { get; set; }
        public string ProviderName { get; set; }
    }
}
