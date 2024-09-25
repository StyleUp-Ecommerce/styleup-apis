using CleanBase.Core.ViewModels.Profiles;
using Core.Entities;
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

        }
    }
}
