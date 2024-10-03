using CleanBase.Core.Data.Policies.Base;
using CleanBase.Core.Data.UnitOfWorks;
using CleanBase.Core.Infrastructure.Policies;
using CleanBase.Core.Services.Core.Base;
using CleanBase.Core.Services.Core;
using CleanBase.Core.Services.Storage;
using Core.Data.Repositories;
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
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using Microsoft.Extensions.Configuration;

namespace TestProject1
{
    public static class Program
    {
        public static ServiceProvider SetUpTest()
        {

            var serviceCollection = new ServiceCollection();
            serviceCollection.AddScoped<ICartRepository, CartRepository>();
            serviceCollection.AddScoped<ICartService, CartService>();
            serviceCollection.AddScoped<ICustomCanvasRepository, CustomCanvasRepository>();
            serviceCollection.AddScoped<ICustomCanvasService, CustomCanvasService>();

            Log.Logger = new LoggerConfiguration()
           .CreateLogger();
            serviceCollection.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            serviceCollection.AddDbContext<AppDbContext>(options =>
            {
                options.UseNpgsql(connectionString); // Sử dụng chuỗi kết nối từ configuration
            });

            serviceCollection.AddSingleton<Serilog.ILogger>(Log.Logger);
            serviceCollection.AddScoped<ISmartLogger, SmartLogger>();
            serviceCollection.AddScoped<IIdentityProvider, IdentityProvider>();
            serviceCollection.AddScoped<IPolicyFactory, PolicyFactory>();
            serviceCollection.AddScoped<ICoreProvider, CoreProvider>();
            serviceCollection.AddScoped(typeof(IStorageService<>), typeof(StorageService<>));
            serviceCollection.AddScoped<IUnitOfWork, UnitOfWorkGeneral>();


            return  serviceCollection.BuildServiceProvider();

            
        }
    }
}
