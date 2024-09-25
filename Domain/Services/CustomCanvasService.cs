using CleanBase.Core.Data.UnitOfWorks;
using CleanBase.Core.Domain.Domain.Services.GenericBase;
using CleanBase.Core.Services.Core.Base;
using Core.Entities;
using Core.Services;
using Core.ViewModels.Requests.CustomCanvas;
using Core.ViewModels.Responses.CustomCanvas;
using Domain.Extensions.Linq;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Domain.Services
{
    public class CustomCanvasService : ServiceBase<CustomCanvas, CustomCanvasRequest, CustomCanvasResponse, GetAllCustomCanvasRequest>, ICustomCanvasService
    {
        public CustomCanvasService(ICoreProvider coreProvider, IUnitOfWork unitOfWork) : base(coreProvider, unitOfWork)
        {
        }

        public async Task<Dictionary<Guid, CustomCanvas>> GetDictionaryByIds(List<Guid> ids, Expression<Func<CustomCanvas, object>>[] excludedProperties)
        {
            var canvases = await Repository.Where(canvas => ids.Contains(canvas.Id))
                                            .ExcludeProperties(excludedProperties)
                                            .ToListAsync();

            var canvasDictionary = canvases.ToDictionary(canvas => canvas.Id);

            return canvasDictionary;
        }
    }
}
