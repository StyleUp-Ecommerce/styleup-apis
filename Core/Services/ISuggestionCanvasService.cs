using CleanBase.Core.Services.Core.Generic;
using Core.Entities;
using Core.ViewModels.Requests.SuggestionCanvas;
using Core.ViewModels.Responses.SuggestionCanvas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public interface ISuggestionCanvasService : IServiceBase<SuggestionCanvas, SuggestionCanvasRequest, SuggestionCanvasResponse, GetAllSuggestionCanvasRequest>
    {
    }
}
