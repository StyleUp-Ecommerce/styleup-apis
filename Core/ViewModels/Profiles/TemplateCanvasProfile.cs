using CleanBase.Core.ViewModels.Profiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Core.Entities;
using Core.ViewModels.Requests.TemplateCanvas;
using Core.ViewModels.Responses.TemplateCanvas;
using CleanBase.Core.Domain.Generic;

namespace Core.ViewModels.Profiles
{
    public class TemplateCanvasProfile : ProfileBase
    {
        protected override void DefaultMapping()
        {
            CreateMap<TemplateCanvasRequest, TemplateCanvas>();
            CreateMap<TemplateCanvas, TemplateCanvasResponse>()
                .ForMember(dest => dest.Provider, opt => opt.MapFrom(src => src.Provider));
            CreateMap<ListResult<TemplateCanvas>, ListResult<TemplateCanvasResponse>>();

        }
    }
}
