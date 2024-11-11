using CleanBase.Core.Data.UnitOfWorks;
using CleanBase.Core.Domain.Domain.Services.GenericBase;
using CleanBase.Core.Domain.Exceptions;
using CleanBase.Core.Extensions;
using CleanBase.Core.Services.Core.Base;
using Core.Caching;
using Core.Caching.Strategies.Cart;
using Core.Caching.Strategies.Provider;
using Core.Data.Repositories;
using Core.Entities;
using Core.Services;
using Core.ViewModels.Requests.Cart;
using Core.ViewModels.Responses.Cart;
using Core.ViewModels.Responses.CartItem;
using Core.ViewModels.Responses.CustomCanvas;
using Domain.Extensions.Linq;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Domain.Services
{
    public class CartService : ServiceBase<Cart, CartRequest, CartResponse, GetAllCartRequest>, ICartService
    {
        private readonly ICustomCanvasService _customCanvasService;
        private readonly ICacheProvider _cacheProvider;

        public CartService(ICoreProvider coreProvider, IUnitOfWork unitOfWork, ICustomCanvasService customCanvasService, ICacheProvider cacheProvider) : base(coreProvider, unitOfWork)
        {
            _cacheProvider = cacheProvider;
            _customCanvasService = customCanvasService;
        }

        public async Task<CartResponse> AddToCart(AddToCartRequest request)
        {
            request.NormalizeData();
                var authorId = IdentityProvider.Identity.UserId;

                var validProduct = await _customCanvasService.GetByIdAsync(request.CustomCanvasId)
                                    ?? throw new DomainException("Product does not exist", null, null, 400, null);

                var cart = await Repository
                            .Where(p => p.AuthorId == authorId)
                            .Include(p => p.CartItems)
                            .SingleOrDefaultAsync()
                            ?? new Cart
                            {
                                AuthorId = authorId,
                                CartItems = new List<CartItem>()
                            };

                var existingItem = cart?.CartItems?.FirstOrDefault(ci =>
                                        ci.CustomCanvasId == request.CustomCanvasId
                                        && string.Equals(request.Size, ci.Size, StringComparison.OrdinalIgnoreCase));

                if (existingItem is not null)
                {
                    existingItem.Quantity += request.Quantity;

                    if (existingItem.Quantity <= 0)
                        cart?.CartItems?.Remove(existingItem);
                }
                else
                {
                    cart?.CartItems?.Add(new CartItem
                    {
                        Quantity = request.Quantity,
                        Size = request.Size.ToUpper(),
                        CustomCanvasId = request.CustomCanvasId
                    });
                }

                var canvasIds = cart?.CartItems?.Select(ci => ci.CustomCanvasId).ToList();

                var productDict = (await _customCanvasService.GetDictionaryByIds(
                    canvasIds,
                    new Expression<Func<CustomCanvas, object>>[] { canvas => canvas.Content }));


                if (cart?.Id == Guid.Empty)
                    Repository.Add(cart);

                await UnitOfWork.SaveChangesAsync();

                return new CartResponse
                {
                    Id = cart.Id,
                    Items = cart.CartItems
                        .Select(ci =>
                        {
                            if (!productDict.TryGetValue(ci.CustomCanvasId, out var product))
                            {
                                return null;
                            }

                            return new CartItemResponse
                            {
                                Quantity = ci.Quantity,
                                Size = ci.Size.ToUpper(),
                                CustomCanvas = new GetCanvasInfoCartResponse
                                {
                                    Id = product.Id,
                                    Price = product.Price,
                                    Images = product.Images.Split(",").ToList(),
                                    Name = product?.Name ?? ""
                                }
                            };
                    })
                    .Where(item => item != null)
                    .Select(item => item!)
                    .ToList(),

                    TotalPrice = cart.CartItems.Sum(ci =>
                    {
                        if (!productDict.TryGetValue(ci.CustomCanvasId, out var product))
                        {
                            return 0m;
                        }
                        return product.Price * ci.Quantity;
                    })
                };
            }

        public async Task<CartResponse> GetCartByUser()
        {
            var authorId = IdentityProvider.Identity.UserId;

            var cart =  await Repository
                .Where(p => p.AuthorId == authorId)
                .Include(p => p.CartItems)
                .SingleOrDefaultAsync()
                ?? new Cart
                {
                    AuthorId = authorId,
                    CartItems = new List<CartItem>()
                };

            var canvasIds = cart.CartItems.Select(ci => ci.CustomCanvasId).ToList();

            Expression<Func<CustomCanvas, object>>[] excludedProperties = new Expression<Func<CustomCanvas, object>>[]
                {
                        canvas => canvas.Content
                };

            var customCanvasData = await UnitOfWork.Repository<ICustomCanvasRepository>().Where(canvas => canvasIds.Contains(canvas.Id))
                                .ExcludeProperties(excludedProperties)
                                .ToListAsync();

            var productDict = customCanvasData.ToDictionary(canvas => canvas.Id);

            return new CartResponse
            {
                Id = cart.Id,
                Items = cart.CartItems
                    .Select(ci =>
                    {
                        if (!productDict.TryGetValue(ci.CustomCanvasId, out var product))
                        {
                            return null;
                        }

                        return new CartItemResponse
                        {
                            Quantity = ci.Quantity,
                            Size = ci.Size.ToUpper(),
                            CustomCanvas = new GetCanvasInfoCartResponse
                            {
                                Id = product.Id,
                                Price = product.Price,
                                Images = product.Images.Split(",").ToList(),
                                Name = product?.Name
                            }
                        };
                    })
                    .Where(item => item != null)
                    .Select(item => item!)
                    .ToList(),

                TotalPrice = cart.CartItems.Sum(ci =>
                {
                    if (!productDict.TryGetValue(ci.CustomCanvasId, out var product))
                    {
                        return 0m;
                    }
                    return product.Price * ci.Quantity;
                })
            };
        }


    }
}
