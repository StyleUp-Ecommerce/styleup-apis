using CleanBase.Core.Services.Core.Generic;
using Core.Entities;
using Core.ViewModels.Requests.OrderItem;
using Core.ViewModels.Responses.OrderItem;

namespace Core.Services
{
    public interface IOrderItemService : IServiceBase<OrderItem, OrderItemRequest, OrderItemResponse, OrderItemGetAllRequest>
    {

    }
}