using CleanBase.Core.Data.UnitOfWorks;
using CleanBase.Core.Domain.Domain.Services.GenericBase;
using CleanBase.Core.Services.Core.Base;
using Core.Entities;
using Core.Services;
using Core.ViewModels.Requests.CartItem;
using Core.ViewModels.Responses.CartItem;

namespace Domain.Services
{
    public class CartItemService : ServiceBase<CartItem, CartItemRequest, CartItemResponse, GetAllCartItemRequest>, ICartItemService
    {
        public CartItemService(ICoreProvider coreProvider, IUnitOfWork unitOfWork) : base(coreProvider, unitOfWork)
        {
        }
    }
}
