using CleanBase.Core.Data.UnitOfWorks;
using CleanBase.Core.Domain.Domain.Services.GenericBase;
using CleanBase.Core.Domain.Exceptions;
using CleanBase.Core.Extensions;
using CleanBase.Core.Services.Core.Base;
using Core.Entities;
using Core.Services;
using Core.ViewModels.Requests.Cart;
using Core.ViewModels.Responses.Cart;
using Core.ViewModels.Responses.CartItem;
using Core.ViewModels.Responses.CustomCanvas;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Domain.Services
{
    public class CartService : ServiceBase<Cart, CartRequest, CartResponse, GetAllCartRequest>, ICartService
    {
        ICustomCanvasService _customCanvasService;
        public CartService(ICoreProvider coreProvider, IUnitOfWork unitOfWork, ICustomCanvasService customCanvasService) : base(coreProvider, unitOfWork)
        {
            _customCanvasService = customCanvasService;
        }

        public async Task<CartResponse> AddToCart(AddToCartRequest request)
        {
            request.NormalizeData();
            var authorId = new Guid("d7749509-d3c4-4c4f-8870-88997e75fcce");

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

            var existingItem = cart.CartItems.FirstOrDefault(ci => 
                                    ci.CustomCanvasId == request.CustomCanvasId 
                                    && string.Equals(request.Size, ci.Size, StringComparison.OrdinalIgnoreCase));

            if (existingItem is not null)
            {
                existingItem.Quantity += request.Quantity;

                if (existingItem.Quantity <= 0)
                    cart.CartItems.Remove(existingItem);
            }
            else
            {
                cart.CartItems.Add(new CartItem
                {
                    CartId = cart.Id,
                    Quantity = request.Quantity,
                    Size = request.Size.ToUpper(),
                    CustomCanvasId = request.CustomCanvasId
                });
            }

            var canvasIds = cart.CartItems.Select(ci => ci.CustomCanvasId).ToList();

            var productDict = (await _customCanvasService.GetDictionaryByIds(
                canvasIds, 
                new Expression<Func<CustomCanvas, object>>[] { canvas => canvas.Content }));


            if (cart.Id == Guid.Empty)
                Repository.Add(cart);

            await UnitOfWork.SaveChangesAsync();

            return new CartResponse
            {
                Id = cart.Id,
                Items = cart.CartItems.Select(ci => {

                    var product = productDict[ci.CustomCanvasId]; 
                    return new CartItemResponse
                    {
                        Quantity = ci.Quantity,
                        Size = ci.Size.ToUpper(),
                        CustomCanvas = new GetCanvasInfoCartResponse
                        {
                            Id= product.Id,
                            Price = product.Price,
                            Images = product.Images.Split(",").ToList(),
                            Name = product.Name
                        }
                    };
                }).ToList(),
                TotalPrice = cart.CartItems.Sum(ci => ci.Quantity * ci.CustomCanvas.Price) 
            };
        }


        public async Task<CartResponse> GetCartById(Guid id)
        {
            var authorId = new Guid("d7749509-d3c4-4c4f-8870-88997e75fcce");

            var cart = await Repository
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
            var productDict = (await _customCanvasService.GetDictionaryByIds(canvasIds, excludedProperties));

            return new CartResponse
            {
                Id = cart.Id,
                Items = cart.CartItems.Select(ci =>
                {

                    var product = productDict[ci.CustomCanvasId];
                    return new CartItemResponse
                    {
                        Quantity = ci.Quantity,
                        Size = ci.Size,
                        CustomCanvas = new GetCanvasInfoCartResponse
                        {
                            Id = product.Id,
                            Price = product.Price,
                            Images = product.Images.Split(",").ToList(),
                            Name = product.Name
                        }
                    };
                }).ToList(),
                TotalPrice = cart.CartItems.Sum(ci => ci.Quantity * ci.CustomCanvas.Price)
            };
        }


    }
}
