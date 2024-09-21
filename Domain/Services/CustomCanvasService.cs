using CleanBase.Core.Data.UnitOfWorks;
using CleanBase.Core.Domain.Domain.Services.GenericBase;
using CleanBase.Core.Services.Core.Base;
using Core.Entities;
using Core.Services;
using Core.ViewModels.Requests.CustomCanvas;
using Core.ViewModels.Responses.CustomCanvas;

namespace Domain.Services
{
    public class CustomCanvasService : ServiceBase<CustomCanvas, CustomCanvasRequest, CustomCanvasResponse, GetAllCustomCanvasRequest>, ICustomCanvasService
    {
        public CustomCanvasService(ICoreProvider coreProvider, IUnitOfWork unitOfWork) : base(coreProvider, unitOfWork)
        {
        }
    }
}
