using CleanBase.Core.Domain.Generic;
using CleanBase.Core.ViewModels.Profiles;
using Core.Entities;
using Core.ViewModels.Requests.SuggestionCanvas;
using Core.ViewModels.Responses.SuggestionCanvas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModels.Profiles
{
    public class SuggestionCanvasProfile : ProfileBase
    {
        protected override void DefaultMapping()
        {
            CreateMap<SuggestionCanvasRequest, SuggestionCanvas>();

            CreateMap<SuggestionCanvasRequest, SuggestionCanvasResponse>();

            CreateMap<SuggestionCanvas, SuggestionCanvasResponse>();

            CreateMap<ListResult<SuggestionCanvas>, ListResult<SuggestionCanvasResponse>>();


        }
    }
}
