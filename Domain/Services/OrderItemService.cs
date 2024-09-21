using CleanBase.Core.Data.UnitOfWorks;
using CleanBase.Core.Domain.Domain.Services.GenericBase;
using CleanBase.Core.Services.Core.Base;
using Core.Entities;
using Core.Services;
using Core.ViewModels.Requests.OrderItem;
using Core.ViewModels.Responses.OrderItem;

namespace Domain.Services
{
    public class OrderItemService : ServiceBase<OrderItem, OrderItemRequest, OrderItemResponse, OrderItemGetAllRequest>, IOrderItemService
    {
        public OrderItemService(ICoreProvider coreProvider, IUnitOfWork unitOfWork) : base(coreProvider, unitOfWork)
        {
        }
    }
}
