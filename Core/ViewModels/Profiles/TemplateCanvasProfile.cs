using AutoMapper;
using CleanBase.Core.Domain.Generic;
using CleanBase.Core.ViewModels.Profiles;
using Core.Entities;
using Core.Helpers;
using Core.ViewModels.Requests.TemplateCanvas;
using Core.ViewModels.Responses.CustomCanvas;
using Core.ViewModels.Responses.TemplateCanvas;

namespace Core.ViewModels.Profiles
{
    public class TemplateCanvasProfile : ProfileBase
    {
        protected override void DefaultMapping()
        {
            CreateMap<TemplateCanvasRequest, TemplateCanvas>() 
                .ForMember(dest => dest.Images, opt => opt.MapFrom(src => StringSpliter.MergeImageString(src.Images)));

            CreateMap<TemplateCanvas, TemplateCanvasResponse>()
                .ForMember(dest => dest.Provider, opt => opt.MapFrom(src => src.Provider))
                .ForMember(dest => dest.Images, opt => opt.MapFrom(src => StringSpliter.SplitImageString(src.Images)));

            CreateMap<ListResult<TemplateCanvas>, ListResult<TemplateCanvasResponse>>();

            CreateMap<List<TemplateCanvasResponse>, ListResult<TemplateCanvasResponse>>();

            CreateMap<TemplateCanvas, GetAllTemplateCanvasResponse>()
                .ForMember(dest => dest.Images, opt => opt.MapFrom(src => StringSpliter.SplitImageString(src.Images)));

        }
    }
}
