using CleanBase.Core.Entities;
using Core.ViewModels.Responses.Provider;

namespace Core.ViewModels.Responses.TemplateCanvas
{
    public class GetTemplateCanvasReponse : EntityNameAuditActive
    {
        public string Descriptions { get; set; }
        public string Image { get; set; }

        public string Content { get; set; }

        public ProviderResponse Provider { get; set; }
    }
}
