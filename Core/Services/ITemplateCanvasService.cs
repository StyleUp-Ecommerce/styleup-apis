using CleanBase.Core.Domain.Generic;
using CleanBase.Core.Services.Core.Generic;
using Core.Entities;
using Core.ViewModels.Requests.TemplateCanvas;
using Core.ViewModels.Responses.TemplateCanvas;

namespace Core.Services
{
    public interface ITemplateCanvasService : IServiceBase<TemplateCanvas, TemplateCanvasRequest, TemplateCanvasResponse, GetAllTemplateCanvasRequest>
    {
        public Task<ListResult<GetAllTemplateCanvasResponse>> GetAllTemplateCanvasAsync(GetAllTemplateCanvasRequest request);
        public Task<GetTemplateCanvasProductResponse> GetTemplateCanvasProductAsync(Guid request);
    }
}