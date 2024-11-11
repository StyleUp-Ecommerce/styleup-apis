using CleanBase.Core.Services.Core.Generic;
using Core.Entities;
using Core.ViewModels.Requests.Order;
using Core.ViewModels.Responses.Order;

namespace Core.Services
{
    public interface IOrderService : IServiceBase<Order, OrderRequest, GetOrderResponse, OrderGetAllRequest>
    {
        public Task<OrderResponse> Ordering (OrderRequest request);
        public Task<GetOrderByCodeResponse> GetOrderByCode(string code);

        Task<bool> ChangeOrderStatus(ChangeOrderStatusRequest request);
    }
}