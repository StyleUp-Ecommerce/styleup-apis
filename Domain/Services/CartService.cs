using CleanBase.Core.Data.UnitOfWorks;
using CleanBase.Core.Domain.Domain.Services.GenericBase;
using CleanBase.Core.Services.Core.Base;
using Core.Entities;
using Core.Services;
using Core.ViewModels.Requests.Cart;
using Core.ViewModels.Responses.Cart;

namespace Domain.Services
{
    public class CartService : ServiceBase<Cart, CartRequest, CartResponse, GetAllCartRequest>, ICartService
    {
        public CartService(ICoreProvider coreProvider, IUnitOfWork unitOfWork) : base(coreProvider, unitOfWork)
        {
        }

        public void Hhaha()
        {

        }
    }
}
