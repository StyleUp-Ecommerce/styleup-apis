using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Moq;
using Xunit;
using Core.Entities;
using Core.Services;
using Core.ViewModels.Requests.Cart;
using Core.ViewModels.Responses.CartItem;
using Core.ViewModels.Responses.CustomCanvas;
using CleanBase.Core.Data.Repositories;
using CleanBase.Core.Data.UnitOfWorks;
using CleanBase.Core.Domain.Exceptions;
using CleanBase.Core.Services.Core.Base;
using Domain.Services;
using CleanBase.Core.Domain;
using System.Security.Claims;

namespace TestProject1
{
    public class CartServiceTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<ICustomCanvasService> _mockCustomCanvasService;
        private readonly Mock<IRepository<Cart>> _mockCartRepository;
        private readonly Mock<ICoreProvider> _mockCoreProvider;
        private readonly CartService _cartService;

        public CartServiceTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockCustomCanvasService = new Mock<ICustomCanvasService>();
            _mockCartRepository = new Mock<IRepository<Cart>>();
            _mockCoreProvider = new Mock<ICoreProvider>();

            // Mock the IdentityProvider to return a valid IdentityInfo with a UserId
            var mockIdentityProvider = new Mock<IIdentityProvider>();

            // Create a fake ClaimsPrincipal with a UserId
            var claims = new List<Claim>
    {
        new Claim("USER_ID", Guid.NewGuid().ToString()), // Mocked UserId
        new Claim(ClaimTypes.Name, "TestUser")
    };
            var identity = new ClaimsIdentity(claims, "TestAuthType");
            var claimsPrincipal = new ClaimsPrincipal(identity);

            var identityInfo = new IdentityInfo(claimsPrincipal);

            mockIdentityProvider.Setup(id => id.Identity).Returns(identityInfo);

            // Setup the CoreProvider to return the mock IdentityProvider
            _mockCoreProvider.Setup(cp => cp.IdentityProvider).Returns(mockIdentityProvider.Object);

            // Setup the UnitOfWork to return the mocked repository
            _mockUnitOfWork.Setup(uow => uow.GetRepositoryByEntityType<Cart>())
                           .Returns(_mockCartRepository.Object);

            // Instantiate the CartService with the mocked dependencies
            _cartService = new CartService(_mockCoreProvider.Object, _mockUnitOfWork.Object, _mockCustomCanvasService.Object);
        }

        [Fact]
        public async Task AddToCart_ShouldAddItem_WhenProductExists()
        {
            // Arrange
            var request = new AddToCartRequest
            {
                CustomCanvasId = Guid.NewGuid(),
                Quantity = 1,
                Size = "M"
            };

            var canvas = new CustomCanvas
            {
                Id = request.CustomCanvasId,
                Price = 100.0m,
                Images = "image1.jpg,image2.jpg",
                Name = "Test Canvas"
            };

            var existingCart = new Cart
            {
                AuthorId = Guid.Parse("USER_ID"), // Mocked UserId
                CartItems = new List<CartItem>()
            };

            // Setup the GetByIdAsync method to return a valid product
            _mockCustomCanvasService.Setup(s => s.GetByIdAsync(It.IsAny<Guid>()))
                                    .ReturnsAsync(canvas);

            // Setup the repository to return an existing cart
            var cartList = new List<Cart> { existingCart };
            var mockCartDbSet = TestHelper.CreateMockDbSet(cartList);

            _mockCartRepository.Setup(r => r.Where(It.IsAny<Expression<Func<Cart, bool>>>()))
                               .Returns(mockCartDbSet.Object);

            // Setup the GetDictionaryByIds to return a product dictionary
            _mockCustomCanvasService.Setup(s => s.GetDictionaryByIds(It.IsAny<List<Guid>>(), It.IsAny<Expression<Func<CustomCanvas, object>>[]>()))
                                    .ReturnsAsync(new Dictionary<Guid, CustomCanvas>
                                    {
                                { request.CustomCanvasId, canvas }
                                    });

            // Act
            var result = await _cartService.AddToCart(request);

            // Assert
            Assert.NotNull(result);
            Assert.Single(result.Items);
            Assert.Equal(request.CustomCanvasId, result.Items.First().CustomCanvas.Id);
            Assert.Equal(1, result.Items.First().Quantity);
            Assert.Equal(100.0m, result.TotalPrice);

            // Verify that SaveChangesAsync was called
            _mockUnitOfWork.Verify(uow => uow.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task AddToCart_ShouldThrowDomainException_WhenProductDoesNotExist()
        {
            // Arrange
            var request = new AddToCartRequest
            {
                CustomCanvasId = Guid.NewGuid(),
                Quantity = 1,
                Size = "M"
            };

            // Setup the GetByIdAsync method to return null (product does not exist)
            _mockCustomCanvasService.Setup(s => s.GetByIdAsync(It.IsAny<Guid>()))
                                    .ReturnsAsync((CustomCanvas)null);

            // Act & Assert
            await Assert.ThrowsAsync<DomainException>(() => _cartService.AddToCart(request));

            // Verify that SaveChangesAsync was never called
            _mockUnitOfWork.Verify(uow => uow.SaveChangesAsync(), Times.Never);
        }
    }
}
