using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
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
using Core.Data.Repositories;
using CleanBase.Core.Data.Policies.Base;
using AutoMapper;

namespace TestProject1
{
    public class CartServiceTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<ICustomCanvasService> _mockCustomCanvasService;
        private readonly Mock<ICartRepository> _mockCartRepository;
        private readonly Mock<ICoreProvider> _mockCoreProvider;
        private readonly Mock<ISmartLogger> _mockSmartLogger;
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<IServiceProvider> _mockServiceProvider;
        private readonly Mock<IPolicyFactory> _mockPolicyFactory;
        private readonly Mock<IIdentityProvider> _mockIdentityProvider;
        private readonly CartService _cartService;

        public CartServiceTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockCustomCanvasService = new Mock<ICustomCanvasService>();
            _mockCartRepository = new Mock<ICartRepository>();
            _mockCoreProvider = new Mock<ICoreProvider>();
            _mockSmartLogger = new Mock<ISmartLogger>();
            _mockMapper = new Mock<IMapper>();
            _mockServiceProvider = new Mock<IServiceProvider>();
            _mockPolicyFactory = new Mock<IPolicyFactory>();
            _mockIdentityProvider = new Mock<IIdentityProvider>();

            // Tạo một fake ClaimsPrincipal với các claims cần thiết
            var claims = new List<Claim>
            {
                new Claim("USER_ID", Guid.NewGuid().ToString()),          // UserId
                new Claim("PREFERRED_USERNAME", "TestUser"),             // UserName
                new Claim("SUB", "sub-value"),                            // Sub
                new Claim("ROLE", "Admin"),                               // Role
                new Claim("ROLE", "User"),                                // Role
                new Claim("PERMISSION ", "Read"),                         // Permission
                new Claim("PERMISSION ", "Write")                         // Permission
            };
            var identity = new ClaimsIdentity(claims, "TestAuthType");
            var claimsPrincipal = new ClaimsPrincipal(identity);

            var identityInfo = new IdentityInfo(claimsPrincipal);

            // Setup IdentityProvider để trả về IdentityInfo đã tạo
            _mockIdentityProvider.Setup(id => id.Identity).Returns(identityInfo);

            // Setup CoreProvider để trả về các mock phụ thuộc
            _mockCoreProvider.Setup(cp => cp.IdentityProvider).Returns(_mockIdentityProvider.Object);
            _mockCoreProvider.Setup(cp => cp.Logger).Returns(_mockSmartLogger.Object);
            _mockCoreProvider.Setup(cp => cp.Mapper).Returns(_mockMapper.Object);
            _mockCoreProvider.Setup(cp => cp.ServiceProvider).Returns(_mockServiceProvider.Object);
            _mockCoreProvider.Setup(cp => cp.PolicyFactory).Returns(_mockPolicyFactory.Object);

            // Setup UnitOfWork để trả về CartRepository mock khi gọi GetRepositoryByEntityType<Cart>()
            _mockUnitOfWork.Setup(uow => uow.GetRepositoryByEntityType<Cart>())
                           .Returns(_mockCartRepository.Object);

            // Instantiate CartService với các mock đã setup
            _cartService = new CartService(_mockCoreProvider.Object, _mockUnitOfWork.Object, _mockCustomCanvasService.Object);
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

            // Setup GetByIdAsync để trả về null (sản phẩm không tồn tại)
            _mockCustomCanvasService.Setup(s => s.GetByIdAsync(It.IsAny<Guid>()))
                                    .ReturnsAsync((CustomCanvas)null);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<DomainException>(() => _cartService.AddToCart(request));

            // Verify rằng SaveChangesAsync không được gọi
            _mockUnitOfWork.Verify(uow => uow.SaveChangesAsync(), Times.Never);

            // Kiểm tra thông báo lỗi
            Assert.Equal("Product does not exist", exception.Message);
        }

        [Fact]
        public async Task AddToCart_ShouldAddItem_WhenProductExists()
        {
            // Arrange
            var request = new AddToCartRequest
            {
                CustomCanvasId = Guid.NewGuid(), // Một CustomCanvas ID giả lập
                Quantity = 2, // Thêm 2 sản phẩm
                Size = "M"
            };

            var mockCustomCanvas = new CustomCanvas
            {
                Id = request.CustomCanvasId,
                Name = "Test Canvas",
                Price = 100.0m,
                Images = "image1.jpg,image2.jpg"
            };

            // Mock GetByIdAsync để trả về sản phẩm hợp lệ
            _mockCustomCanvasService.Setup(s => s.GetByIdAsync(It.IsAny<Guid>()))
                                    .ReturnsAsync(mockCustomCanvas);

            // Lấy UserId từ IdentityProvider
            var userId = _mockIdentityProvider.Object.Identity.UserId;

            // Mock một giỏ hàng hiện tại với AuthorId là userId
            var mockCart = new Cart
            {
                Id = Guid.NewGuid(),
                AuthorId = userId,
                CartItems = new List<CartItem>()
            };

            // Setup IAsyncQueryProvider cho IQueryable
            var queryableCartList = new List<Cart> { mockCart }.AsQueryable();
            var mockAsyncEnumerable = new TestAsyncEnumerable<Cart>(queryableCartList);

            _mockCartRepository.Setup(repo => repo.Where(It.IsAny<Expression<Func<Cart, bool>>>()))
                               .Returns(mockAsyncEnumerable);

            // Setup phương thức Update để không thực hiện hành động gì thêm
            _mockCartRepository.Setup(repo => repo.Update(It.IsAny<Cart>(), It.IsAny<bool>()))
                               .Callback<Cart, bool>((cart, saveChanges) =>
                               {
                                   // Giả lập việc cập nhật giỏ hàng nếu cần thiết
                                   // Trong trường hợp này, chúng ta có thể không làm gì
                               });

            // Setup SaveChangesAsync để trả về Task.CompletedTask
            _mockUnitOfWork.Setup(uow => uow.SaveChangesAsync())
                           .Returns(Task.CompletedTask);

            // Act
            var response = await _cartService.AddToCart(request);

            // Assert
            // Verify rằng phương thức Update của repository được gọi đúng một lần
            _mockCartRepository.Verify(repo => repo.Update(It.IsAny<Cart>(), It.IsAny<bool>()), Times.Once);

            // Verify rằng SaveChangesAsync của UnitOfWork được gọi đúng một lần
            _mockUnitOfWork.Verify(uow => uow.SaveChangesAsync(), Times.Once);

            // Kiểm tra rằng mục giỏ hàng đã được thêm vào giỏ hàng
            Assert.Single(mockCart.CartItems);
            var cartItem = mockCart.CartItems.First();
            Assert.Equal(request.CustomCanvasId, cartItem.CustomCanvasId);
            Assert.Equal(request.Quantity, cartItem.Quantity);
            Assert.Equal(request.Size.ToUpper(), cartItem.Size); // Size được chuyển thành chữ in hoa trong AddToCart
        }

    }
}
