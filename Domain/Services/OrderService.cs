    using CleanBase.Core.Data.UnitOfWorks;
using CleanBase.Core.Domain.Domain.Services.GenericBase;
using CleanBase.Core.Domain.Exceptions;
using CleanBase.Core.Helpers;
using CleanBase.Core.Services.Core.Base;
using Core.Constants;
using Core.Data.Repositories;
using Core.Entities;
using Core.Helpers;
using Core.Identity.Email.Enums;
using Core.Identity.Email.Interfaces;
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
        public readonly IEmailService _emailService;
        public OrderService(ICoreProvider coreProvider, 
            IUnitOfWork unitOfWork, 
            ICustomCanvasService customCanvasService, 
            IVoucherService voucherService,
            IEmailService emailService) : 
            base(coreProvider, unitOfWork)
        {
            _customCanvasService = customCanvasService;
            _voucherService = voucherService;
            _emailService = emailService;
        }

        public async Task<OrderResponse> Ordering(OrderRequest request)
        {
            using (var tran = this.UnitOfWork.BeginTransaction())
            {
                try
                {
                    var customCanvasIds = request.Items.Select(i => i.CustomCanvasId).ToList();

                    var canvasDic = await _customCanvasService.GetDictionaryByIds(customCanvasIds,
                        new Expression<Func<CustomCanvas, object>>[] { canvas => canvas.Content });

                    if (canvasDic is null)
                    {
                        throw new DomainException("No products exist", null, null, 400, null);
                    }

                    //Check valid voucher

                    var voucher = !string.IsNullOrEmpty(request?.VoucherCode)
                            ? await _voucherService.GetVoucherByCode(request.VoucherCode)
                            : null;
                    // Migrate cart item to order item


                    var currenOrder = Mapper.Map<Order>(request, opt =>
                    {
                        opt.AfterMap((src, dest) =>
                        {
                            dest.StatusString = StatusEnum.Pending.ToString();
                            dest.VoucherCode = voucher?.Code ?? null;
                            dest.OrderCode = Randomize.RandomString(5).ToUpper()+ " "+  DateTimeHelper.VnNow().ToString("MM/dd/yyyy");
                            dest.Id = Guid.NewGuid();
                            dest.RecipientMail = request.RecipientEmail;
                        });
                    });

                   
                    var responseItems = request.Items.Select(item =>
                    {
                        var canvasItem = canvasDic[item.CustomCanvasId];
                        var orderItem = Mapper.Map<OrderItemDetailResponse>(item);
                        orderItem.Quantity = item.Quantity;
                        orderItem.Size = item.Size;
                        orderItem.Price = canvasItem.Price;
                        orderItem.Color = canvasItem.ColorString;


                        var newOrderItem = Mapper.Map<OrderItem>(orderItem);
                        newOrderItem.Order = currenOrder;
                        newOrderItem.OrderId = currenOrder.Id;


                        UnitOfWork.Repository<IOrderItemRepository>().Add(newOrderItem);
                        return orderItem;
                    }).ToList();

                    

                    //this.Repository.Update(currenOrder);

                    var oldCartItems = request.Items.Select(i => i.CartItemId).ToList(); 
                    foreach (var itemId in oldCartItems)
                    {
                        UnitOfWork.Repository<ICartItemRepository>().Delete(itemId); 
                    }

              
                    var result = Mapper.Map<OrderResponse>(currenOrder, opt =>
                    {
                        opt.AfterMap((src, dest) =>
                        {
                            dest.Items = responseItems;
                            dest.TotalPrice = responseItems.Sum(p => p.Price * p.Quantity);
                            dest.DiscountValue = voucher?.DiscountValue ?? 0;
                            dest.DiscountType = voucher?.DiscountType;
                            dest.RecipientEmail = currenOrder.RecipientMail;
                        });
                    });

                    //var param = _emailService.GenerateOrderedParameters(result.RecipientName, result);
                    //await _emailService.SendAsync(EmailType.Ordered, result.RecipientEmail, param);

                    tran.Commit();
                    UnitOfWork.SaveChanges();

                    return result;
                }
                catch (Exception ex)
                {
                    Logger.Error(ex.Message, ex);
                    tran.Dispose();
                    throw new Exception("An error occurred while processing your order: " + ex.Message, ex);
                }
            }
        }

    }
}
