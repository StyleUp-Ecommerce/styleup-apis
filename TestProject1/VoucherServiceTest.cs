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
using System.Linq.Expressions;
using Core.Services;
using Microsoft.Extensions.DependencyInjection;
using Core.Data.Repositories;
using CleanBase.Core.Services.Core.Base;
using Infrastructure.Repositories;
using CleanBase.Core.Services.Core;
using CleanBase.Core.Infrastructure.Policies;
using CleanBase.Core.Data.Policies.Base;
using CleanBase.Core.Services.Storage;
using Infrastructure.UnitOfWorks;
using Serilog;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace TestProject1
{
    public class VoucherServiceTests
    {

        private readonly IVoucherService _voucherService;

        public VoucherServiceTests()
        {


            var serviceCollection = new ServiceCollection();
            serviceCollection.AddScoped<IVoucherRepository, VoucherRepository>();
            serviceCollection.AddScoped<IVoucherService, VoucherService>();

            Log.Logger = new LoggerConfiguration()
           .CreateLogger();
            serviceCollection.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            serviceCollection.AddDbContext<AppDbContext>(options =>
            {
                options.UseNpgsql("bỏ connect vô đây test");
            });

            serviceCollection.AddSingleton<Serilog.ILogger>(Log.Logger);
            serviceCollection.AddScoped<ISmartLogger, SmartLogger>();
            serviceCollection.AddScoped<IIdentityProvider, IdentityProvider>();
            serviceCollection.AddScoped<IPolicyFactory, PolicyFactory>();
            serviceCollection.AddScoped<ICoreProvider, CoreProvider>();
            serviceCollection.AddScoped(typeof(IStorageService<>), typeof(StorageService<>));
            serviceCollection.AddScoped<IUnitOfWork, UnitOfWorkGeneral>();


            var serviceProvider = serviceCollection.BuildServiceProvider();

            _voucherService = serviceProvider.GetRequiredService<IVoucherService>();
        }

        [Fact]
        public async Task GetVoucherByCode_ShouldReturnVoucher_WhenVoucherExists()
        {
            var voucherId = new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6");
            var voucher = new Voucher
            {
                Id = voucherId,
                Code = "cc",
                DiscountType = "percentage",
                DiscountValue = 10,
                ExpirationDate = DateTime.UtcNow.AddDays(30)
            };



            var result = await _voucherService.GetVoucherByCode("GIANGSINH2024");

            Assert.NotNull(result);
            Assert.Equal(voucher.Code, result.Code);
        }
    }
}
