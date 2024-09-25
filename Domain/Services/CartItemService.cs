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

        public async Task<bool> CleanCartItemByIds(List<Guid> ids)
        {
            if (ids is null)
            {
                return false;
            }

            var tasks = ids.Select(id => this.Repository.DeleteAsync(id));

            var results = await Task.WhenAll(tasks);

            return results.All(result => result);
        }

    }
}
