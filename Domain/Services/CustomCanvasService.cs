using CleanBase.Core.Data.UnitOfWorks;
using CleanBase.Core.Domain.Domain.Services.GenericBase;
using CleanBase.Core.Services.Core.Base;
using Core.Entities;
using Core.Services;
using Core.ViewModels.Requests.CustomCanvas;
using Core.ViewModels.Responses.CustomCanvas;
using Microsoft.EntityFrameworkCore;

namespace Domain.Services
{
    public class CustomCanvasService : ServiceBase<CustomCanvas, CustomCanvasRequest, CustomCanvasResponse, GetAllCustomCanvasRequest>, ICustomCanvasService
    {
        public CustomCanvasService(ICoreProvider coreProvider, IUnitOfWork unitOfWork) : base(coreProvider, unitOfWork)
        {
        }

        public async Task<ICollection<CustomCanvas>> GetListByIds(List<Guid> ids)
        {
            var canvases = await Repository
                .Where(canvas => ids.Contains(canvas.Id))
                .ToListAsync(); 

            return canvases; 
        }


    }
}
