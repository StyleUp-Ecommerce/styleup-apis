using CleanBase.Core.ViewModels.Profiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Core.Entities;
using Core.ViewModels.Requests.CustomCanvas;
using Core.ViewModels.Responses.CustomCanvas;
using CleanBase.Core.Domain.Generic;

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
        }
    }
}
