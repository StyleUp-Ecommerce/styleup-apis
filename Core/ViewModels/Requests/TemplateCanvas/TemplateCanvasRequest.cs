using CleanBase.Core.ViewModels.Request.Base;

namespace Core.ViewModels.Requests.TemplateCanvas
{
    public class TemplateCanvasRequest : EntityRequestBase
    {
        public string? Descriptions { get; set; }
        public List<string>? Images { get; set; }
        public string? Content { get; set; }
        public Guid? ProviderId { get; set; }

    }
}
