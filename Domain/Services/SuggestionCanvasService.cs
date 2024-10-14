using CleanBase.Core.Data.UnitOfWorks;
using CleanBase.Core.Domain.Domain.Services.GenericBase;
using CleanBase.Core.Services.Core.Base;
using Core.Entities;
using Core.Services;
using Core.ViewModels.Requests.SuggestionCanvas;
using Core.ViewModels.Responses.SuggestionCanvas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class SuggestionCanvasService : ServiceBase<SuggestionCanvas, SuggestionCanvasRequest, SuggestionCanvasResponse, GetAllSuggestionCanvasRequest>, ISuggestionCanvasService
    {
        public SuggestionCanvasService(ICoreProvider coreProvider, IUnitOfWork unitOfWork) : base(coreProvider, unitOfWork)
        {
        }
    }
}
