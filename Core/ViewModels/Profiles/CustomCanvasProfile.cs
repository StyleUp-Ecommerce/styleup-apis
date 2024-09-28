using CleanBase.Core.Domain.Generic;
using CleanBase.Core.ViewModels.Profiles;
using Core.Entities;
using Core.Helpers;
using Core.ViewModels.Requests.CustomCanvas;
using Core.ViewModels.Responses.CustomCanvas;

namespace Core.ViewModels.Profiles
{
    public class CustomCanvasProfile : ProfileBase
    {
        protected override void DefaultMapping()
        {
            CreateMap<CustomCanvasRequest, CustomCanvas>()
                .ForMember(dest => dest.Color, opt => opt.MapFrom(src => src.Color));
            CreateMap<CustomCanvas, GetCustomCanvasResponse>()
                 .ForMember(dest => dest.Color, opt => opt.MapFrom(src => src.Color))
                .ForMember(dest => dest.Images, opt => opt.MapFrom(src => StringSpliter.SplitImageString(src.Images)));
            CreateMap<CustomCanvas, CustomCanvasResponse>()
                 .ForMember(dest => dest.Color, opt => opt.MapFrom(src => src.Color))
                .ForMember(dest => dest.Images, opt => opt.MapFrom(src => StringSpliter.SplitImageString(src.Images)));
            CreateMap<ListResult<CustomCanvas>, ListResult<CustomCanvasResponse>>();
            CreateMap<CustomCanvas, CustomCanvasProductResponse>()
                .ForMember(dest => dest.Color, opt => opt.MapFrom(src => src.ColorString))
                .ForMember(dest => dest.Images, opt => opt.MapFrom(src => StringSpliter.SplitImageString(src.Images)));
        }
    }
}
