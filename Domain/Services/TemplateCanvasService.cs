using CleanBase.Core.Data.UnitOfWorks;
using CleanBase.Core.Domain.Domain.Services.GenericBase;
using CleanBase.Core.Services.Core.Base;
using Core.Entities;
using Core.Services;
using Core.ViewModels.Requests.TemplateCanvas;
using Core.ViewModels.Responses.TemplateCanvas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class TemplateCanvasService : ServiceBase<TemplateCanvas, TemplateCanvasRequest, TemplateCanvasResponse, GetAllTemplateCanvasRequest>, ITemplateCanvasService
    {
        public TemplateCanvasService(ICoreProvider coreProvider, IUnitOfWork unitOfWork) : base(coreProvider, unitOfWork)
        {
        }
    }
}
