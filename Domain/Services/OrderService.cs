using CleanBase.Core.Data.UnitOfWorks;
using CleanBase.Core.Domain.Domain.Services.GenericBase;
using CleanBase.Core.Services.Core.Base;
using Core.Constants;
using Core.Entities;
using Core.Services;
using Core.ViewModels.Requests.Order;
using Core.ViewModels.Responses.Order;
using Core.ViewModels.Responses.OrderItem;
using System.Linq.Expressions;

namespace Domain.Services
{
    public class OrderService : ServiceBase<Order, OrderRequest, OrderResponse, OrderGetAllRequest>, IOrderService
    {
        public readonly ICustomCanvasService _customCanvasService;
        public readonly IVoucherService _voucherService;
        public OrderService(ICoreProvider coreProvider, IUnitOfWork unitOfWork, ICustomCanvasService customCanvasService) : base(coreProvider, unitOfWork)
        {
            _customCanvasService = customCanvasService;
        }

        public async Task<OrderResponse> Ordering(OrderRequest request)
        {
            using(var tran = this.UnitOfWork.BeginTransaction())
            {
                try
                {
                    var customCanvasIds = request.Items.Select(i => i.CustomCanvasId).ToList();


                    var canvasDic = await _customCanvasService.GetDictionaryByIds(
                        customCanvasIds,
                        new Expression<Func<CustomCanvas, object>>[] { canvas => canvas.Content });

                    var responseItems = request.Items.Select(item =>
                     {
                         var orderItem = Mapper.Map<OrderItemDetailResponse>(item);
                         orderItem.Quantity = item.Quantity;
                         orderItem.Size = item.Size;
                         orderItem.Price = canvasDic[item.CustomCanvasId].Price;
                         return orderItem;
                     }).ToList();

                    var voucher = request.VoucherCode is not null
                                    ? await _voucherService.GetVoucherByCode(request.VoucherCode)
                                    : null;

                    tran.Commit();
                    return new OrderResponse
                    {
                        Items = responseItems,
                        OrderStatus = StatusEnum.Pending.ToString(),
                        TotalPrice = 500,
                        DiscountType = voucher.DiscountType,
                        DiscountValue = voucher.DiscountValue,
                        OrderCode = null
                    };
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message, ex);
                }
            }
        }
    }
}
