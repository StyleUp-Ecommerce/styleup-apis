using CleanBase.Core.ViewModels.Response.Base;

namespace Core.ViewModels.Responses.CustomCanvas
{
    public class GetCanvasInfoCartResponse : EntityNameResponseBase
    {
        public decimal Price { get; set; }
        public List<string> Images { get; set; }
    }
}
