using CleanBase.Core.Domain.Generic;
using CleanBase.Core.ViewModels.Profiles;
using Core.Entities;
using Core.ViewModels.Requests.CustomCanvas;
using Core.ViewModels.Responses.CustomCanvas;

namespace Core.ViewModels.Profiles
{
    public class CustomCanvasProfile : ProfileBase
    {
        protected override void DefaultMapping()
        {
            CreateMap<CustomCanvasRequest, CustomCanvas>();
            CreateMap<CustomCanvas, GetCustomCanvasResponse>();
            CreateMap<CustomCanvas, CustomCanvasResponse>();
            CreateMap<ListResult<CustomCanvas>, ListResult<CustomCanvasResponse>>();
            CreateMap<CustomCanvas, CustomCanvasProductResponse>()
                .ForMember(dest => dest.Color, opt => opt.MapFrom(src => src.ColorString));
        }
    }
}
