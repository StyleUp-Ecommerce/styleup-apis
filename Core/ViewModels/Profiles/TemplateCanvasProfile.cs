using AutoMapper;
using CleanBase.Core.Domain.Generic;
using CleanBase.Core.ViewModels.Profiles;
using Core.Entities;
using Core.Helpers;
using Core.ViewModels.Requests.TemplateCanvas;
using Core.ViewModels.Responses.CustomCanvas;
using Core.ViewModels.Responses.Provider;
using Core.ViewModels.Responses.TemplateCanvas;

namespace Core.ViewModels.Profiles
{
    public class TemplateCanvasProfile : ProfileBase
    {
        protected override void DefaultMapping()
        {
            CreateMap<TemplateCanvasRequest, TemplateCanvas>() 
                .ForMember(dest => dest.Images, opt => opt.MapFrom(src => StringSpliter.ListToString(src.Images)));

            CreateMap<TemplateCanvas, TemplateCanvasResponse>()
                .IncludeMembers(src => src.Provider)
                .ForMember(dest => dest.Provider, opt => opt.MapFrom(src => src.Provider))
                .ForMember(dest => dest.Colors, opt => opt.MapFrom(src => string.Join(",",src.CustomCanvas.Select(c => c.Color))))
                .ForMember(dest => dest.Images, opt => opt.MapFrom(src => StringSpliter.StringToList(src.Images)));

            CreateMap<TemplateCanvas, TemplateCanvasFilterResponse>()
                .IncludeMembers(src => src.Provider)
                .ForMember(dest => dest.Provider, opt => opt.MapFrom(src => src.Provider))
                .ForMember(dest => dest.MinPrice, opt => opt.MapFrom(src => src.CustomCanvas.Min(c => c.Price)))
                .ForMember(dest => dest.Colors, opt => opt.MapFrom(src => src.CustomCanvas != null
                    ? string.Join(",", src.CustomCanvas.Select(c => c.Color))
                    : string.Empty))
                .ForMember(dest => dest.Images, opt => opt.MapFrom(src => StringSpliter.StringToList(src.Images)));

            CreateMap<ListResult<TemplateCanvas>, ListResult<TemplateCanvasResponse>>();

            CreateMap<List<TemplateCanvasResponse>, ListResult<TemplateCanvasResponse>>();

            CreateMap<TemplateCanvas, GetAllTemplateCanvasResponse>()
                .ForMember(dest => dest.Images, opt => opt.MapFrom(src => StringSpliter.StringToList(src.Images)));

            CreateMap<Provider, TemplateCanvasResponse>();
            CreateMap<Provider, TemplateCanvasFilterResponse>();

        }
    }
}
