using CleanBase.Core.Domain.Generic;
using CleanBase.Core.ViewModels.Profiles;
using Core.Entities;
using Core.ViewModels.Requests.Order;
using Core.ViewModels.Responses.Order;

namespace Core.ViewModels.Profiles
{
    public class OrderProfile : ProfileBase
    {
        protected override void DefaultMapping()
        {
            CreateMap<OrderRequest, Order>();
            CreateMap<Order, GetOrderResponse>()
                .ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => src.OrderItems));
            CreateMap<Order, OrderResponse>();
            CreateMap<ListResult<Order>, ListResult<OrderResponse>>();

        }
    }
}
