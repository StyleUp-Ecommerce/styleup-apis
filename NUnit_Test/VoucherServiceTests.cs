using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Services;
using Core.ViewModels.Requests.Voucher;
using Core.ViewModels.Responses.Voucher;
using CleanBase.Core.Data.UnitOfWorks;
using CleanBase.Core.Domain.Exceptions;
using CleanBase.Core.Helpers;
using CleanBase.Core.Data.Repositories;
using CleanBase.Core.Services.Core.Base;
using Domain.Services;
using Core.Data.Repositories;
using Infrastructure.Repositories;
using System.Linq.Expressions;

namespace NUnit_Test
{
    [TestFixture]
    public class VoucherServiceTests
    {
        private Mock<IUnitOfWork> _unitOfWorkMock;
        private Mock<IVoucherRepository> _voucherRepositoryMock;
        private Mock<ICoreProvider> _coreProviderMock;
        private VoucherService _voucherService;


        [SetUp]
        public void Setup()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _voucherRepositoryMock = new Mock<IVoucherRepository>();
            _coreProviderMock = new Mock<ICoreProvider>();

         
           
            _unitOfWorkMock.Setup(uow => uow.Repository<IVoucherRepository>())
                .Returns((IVoucherRepository)_voucherRepositoryMock.Object);


            _voucherService = new VoucherService(_coreProviderMock.Object, _unitOfWorkMock.Object);
        }
        [Test]
        public void GetVoucherByCode_InvalidVoucher_ThrowsDomainException()
        {
         
            var invalidVoucherCode = "INVALID_CODE";

            _voucherRepositoryMock.Setup(repo => repo.Where(It.IsAny<Expression<Func<Voucher, bool>>>()))
                .Returns(Enumerable.Empty<Voucher>().AsQueryable());

         
            var exception = Assert.ThrowsAsync<DomainException>(() => _voucherService.GetVoucherByCode(invalidVoucherCode));

            Assert.AreEqual("Voucher not valid", exception.Message);
        }
    }
}