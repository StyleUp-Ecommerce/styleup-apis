using CleanBase.Core.Services.Core.Generic;
using Core.Entities;
using Core.ViewModels.Requests.CustomCanvas;
using Core.ViewModels.Responses.CustomCanvas;
using System.Linq.Expressions;

namespace Core.Services
{
    public interface ICustomCanvasService : IServiceBase<CustomCanvas, CustomCanvasRequest, GetCustomCanvasResponse, GetAllCustomCanvasRequest>
    {
        public Task<Dictionary<Guid, CustomCanvas>> GetDictionaryByIds(List<Guid> ids, Expression<Func<CustomCanvas, object>>[] excludedProperties);

        public Task<Guid> CustomNewTemplate(Guid customCanvasId);
    }
}