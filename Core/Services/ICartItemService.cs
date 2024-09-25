using CleanBase.Core.Services.Core.Generic;
using Core.Entities;
using Core.ViewModels.Requests.CartItem;
using Core.ViewModels.Responses.CartItem;

namespace Core.Services
{
    public interface ICartItemService : IServiceBase<CartItem, CartItemRequest, CartItemResponse, GetAllCartItemRequest>
    {
        Task<bool> CleanCartItemByIds(List<Guid> ids);
    }
}