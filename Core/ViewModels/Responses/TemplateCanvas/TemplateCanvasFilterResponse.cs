using CleanBase.Core.ViewModels.Response.Base;
using Core.Entities;
using Core.ViewModels.Responses.Provider;

namespace Core.ViewModels.Responses.TemplateCanvas
{
    public class TemplateCanvasFilterResponse : EntityAuditNameResponseBase
    {
        public string Descriptions { get; set; }
        public List<string> Images { get; set; }
        public String Colors { get; set; }
        public string Content { get; set; }
        public string TemplateCode { get; set; }
        public ProviderResponse Provider { get; set; }
        public decimal MinPrice { get; set; }
    }
}
