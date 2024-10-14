using CleanBase.Core.Domain.Generic;
using CleanBase.Core.ViewModels.Profiles;
using Core.Entities;
using Core.Helpers;
using Core.ViewModels.Requests.OrderItem;
using Core.ViewModels.Responses.OrderItem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModels.Profiles
{
    public class OrderItemProfile : ProfileBase
    {
        protected override void DefaultMapping()
        {
            CreateMap<OrderItemRequest, OrderItemResponse>();
            CreateMap<OrderItemRequest, OrderItem>();
            CreateMap<OrderItem, OrderItemResponse>();
            CreateMap<OrderItemRequest, OrderItemDetailResponse>();
            CreateMap<ListResult<OrderItem>, ListResult<OrderItemRequest>>();
            CreateMap<OrderItemDetailResponse, OrderItem>()
                .ReverseMap()
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => StringSpliter.StringToList(src.CustomCanvas.Images)[0]));


        }
    }
}
