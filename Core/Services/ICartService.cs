using CleanBase.Core.Services.Core.Generic;
using Core.Entities;
using Core.ViewModels.Requests.Cart;
using Core.ViewModels.Responses.Cart;

namespace Core.Services
{
    public interface ICartService : IServiceBase<Cart, CartRequest, CartResponse, GetAllCartRequest>
    {
        public Task<CartResponse> AddToCart(AddToCartRequest request);
        public Task<CartResponse> GetCartById(Guid id);
    }
}