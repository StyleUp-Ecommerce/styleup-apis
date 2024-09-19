using CleanBase.Core.ViewModels.Profiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Core.Entities;
using Core.ViewModels.Requests.Order;
using Core.ViewModels.Responses.Order;
using CleanBase.Core.Domain.Generic;

namespace Core.ViewModels.Profiles
{
    public class OrderProfile : ProfileBase
    {
        protected override void DefaultMapping()
        {
            CreateMap<OrderRequest, Order>();
            CreateMap<Order, GetOrderResponse>()
                .ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => src.OrderItems));
            CreateMap<Order, OrderResponse>()
                 .ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => src.OrderItems));
            CreateMap<ListResult<Order>, ListResult<OrderResponse>>();

        }
    }
}
