using CleanBase.Core.Data.Policies.Base;
using CleanBase.Core.Data.UnitOfWorks;
using CleanBase.Core.Infrastructure.Policies;
using CleanBase.Core.Services.Core.Base;
using CleanBase.Core.Services.Core;
using CleanBase.Core.Services.Storage;
using Core.Data.Repositories;
using Core.Entities;
using Core.Services;
using Domain.Services;
using Infrastructure.Context;
using Infrastructure.Repositories;
using Infrastructure.UnitOfWorks;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.ViewModels.Requests.Cart;
using Microsoft.EntityFrameworkCore;
using Core.ViewModels.Responses.Cart;
using Microsoft.Extensions.Configuration;

namespace TestProject1
{
    public class CartServiceTests2
    {

        private readonly ICartService _cartService;

        public CartServiceTests2()
        {
            var serviceProvider =  Program.SetUpTest();
            _cartService = serviceProvider.GetRequiredService<ICartService>();
        }

        [Fact]
        public async Task AddToCart_ShouldAddItem_WhenProductExists()
        {
            // Arrange
            var request = new AddToCartRequest
            {
                CustomCanvasId = new Guid("3be59c08-db76-490d-8253-5376061570c2"), // Một CustomCanvas ID giả lập
                Quantity = 2, // Thêm 2 sản phẩm
                Size = "M"
            };               
          

            // Act
            var response = await _cartService.AddToCart(request);


            // Kiểm tra rằng mục giỏ hàng đã được thêm vào giỏ hàng



            var exist = response.Items.Any(i => i.CustomCanvas.Id.Equals(request.CustomCanvasId));


            Assert.Equal(true, exist); // Size được chuyển thành chữ in hoa trong AddToCart
        }

        [Fact]
        public async Task GetCartById_ShouldReturnCart_WhenIdExists()
        {
            // Arrange
            var cartId = "b3a3cb45-c759-446e-a85d-a3d150bbb8e0";

            var request = new Guid(cartId);

            // Act
            var response = await _cartService.GetCartById(request)?? new CartResponse { Id =Guid.NewGuid()} ;


            Assert.Equal(cartId, response.Id.ToString()); // Size được chuyển thành chữ in hoa trong AddToCart
        }

        [Fact]
        public async Task AddToCart_ShouldIncreaseQuantity_WhenProductAlreadyInCart()
        {
            // Arrange

            var cartId = "b3a3cb45-c759-446e-a85d-a3d150bbb8e0";

            var request = new AddToCartRequest
            {
                CustomCanvasId = new Guid("3be59c08-db76-490d-8253-5376061570c2"), // Một CustomCanvas ID giả lập
                Quantity = 2, // Thêm 2 sản phẩm
                Size = "M"
            };

            var cart = await _cartService.GetCartById(new Guid(cartId)) ?? new CartResponse { Id = Guid.NewGuid() };

            int earlyQuantity = cart.Items.FirstOrDefault(i => i.CustomCanvas.Id.Equals(request.CustomCanvasId) && i.Size.Equals("M")).Quantity;

            // Act
            var response = await _cartService.AddToCart(request);


            // Kiểm tra rằng mục giỏ hàng đã được thêm vào giỏ hàng



            var afterQuantity = response.Items.FirstOrDefault(i => i.CustomCanvas.Id.Equals(request.CustomCanvasId) && i.Size.Equals("M")).Quantity;


            Assert.Equal(earlyQuantity+2, afterQuantity); // Size được chuyển thành chữ in hoa trong AddToCart
        }
    }
}
