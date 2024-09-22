using CleanBase.Core.Services.Core.Generic;
using Core.Entities;
using Core.ViewModels.Requests.CustomCanvas;
using Core.ViewModels.Responses.CustomCanvas;

namespace Core.Services
{
    public interface ICustomCanvasService : IServiceBase<CustomCanvas, CustomCanvasRequest, GetCustomCanvasResponse, GetAllCustomCanvasRequest>
    {
        Task<ICollection<CustomCanvas>> GetListByIds(List<Guid> ids);
    }
}