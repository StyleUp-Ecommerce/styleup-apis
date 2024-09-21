using CleanBase.Core.Data.UnitOfWorks;
using CleanBase.Core.Domain.Domain.Services.GenericBase;
using CleanBase.Core.Services.Core.Base;
using Core.Entities;
using Core.Services;
using Core.ViewModels.Requests.Order;
using Core.ViewModels.Responses.Order;

namespace Domain.Services
{
    public class OrderService : ServiceBase<Order, OrderRequest, OrderResponse, OrderGetAllRequest>, IOrderService
    {
        public OrderService(ICoreProvider coreProvider, IUnitOfWork unitOfWork) : base(coreProvider, unitOfWork)
        {
        }
    }
}
