
using Xunit;
using Moq;
using Domain.Services;
using Core.ViewModels.Requests.Voucher;
using Core.ViewModels.Responses.Voucher;
using CleanBase.Core.Data.UnitOfWorks;
using CleanBase.Core.Domain.Exceptions;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using CleanBase.Core.Services.Core.Base;
using Infrastructure.Repositories;
using Core.Data.Repositories;
using System.Linq.Expressions;
using Core.Services;
namespace TestProject1
{

    public class VoucherServiceTests
    {
        private readonly Mock<IVoucherRepository> _voucherRepositoryMock;
        private readonly Mock<ICoreProvider> _coreProviderMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly VoucherService _voucherService;
       // public readonly IVoucherService _voucherService;

        public VoucherServiceTests()
        {
            _voucherRepositoryMock = new Mock<IVoucherRepository>();
            _coreProviderMock = new Mock<ICoreProvider>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
          _voucherService = new VoucherService(_voucherRepositoryMock.Object, _coreProviderMock.Object, _unitOfWorkMock.Object);
        }
        [Fact]
        public async Task GetVoucherByCode_ShouldReturnVoucher_WhenVoucherExists()
        {
            // Arrange
            var voucherId = new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6");
            var voucher = new Voucher
            {
                Id = voucherId,
                Code = "cc",
                DiscountType = "percentage",
                DiscountValue = 10,
                ExpirationDate = DateTime.UtcNow.AddDays(30)
            };
            _voucherRepositoryMock.Setup(repo => repo.Where(It.IsAny<Expression<Func<Voucher, bool>>>()))
                .Returns(new List<Voucher> { voucher }.AsQueryable());
            // Make sure this matches your method signature.

            // Act
            Voucher result = await _voucherService.GetByIdAsync("3fa85f64-5717-4562-b3fc-2c963f66afa6");
       
            // Assert
            Assert.NotNull(result);
            Assert.Equal(voucher.Code, result.Code);


        }

    }





}
