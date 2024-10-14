using CleanBase.Core.Infrastructure.Jobs;
using Core.Identity.Email.Interfaces;
using Core.Identity.Interfaces;
using Core.Services;
using Infrastructure.Commons;
using Infrastructure.Email;
using Infrastructure.Identity.Services;
using Infrastructure.Jobs;
using Infrastructure.Repositories.DI;
using Infrastructure.Transactions;
using Infrastructure.Transactions.PaymentMethod;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class InfrastructureBootstrapper
    {
        public static IServiceCollection RegisterInfrastructureService(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            JobBootStrapper.UseHangfireBackgroundJob(services);

            services.AddScoped<ITransactionService, TransactionService>();
            services.AddScoped<IPaymentService, VNPayService>(provider =>
            {           

                var hashSecret = "IE3N9LTPY78WFWJK7GJO8KQFH3ZQ1IG5"; 
                var tmnCode = "HBTD6P6Q"; 
                var url = "https://sandbox.vnpayment.vn/paymentv2/vpcpay.html"; 
                return new VNPayService(url,tmnCode,hashSecret);
            });


            services.Configure<AppConfig>(configuration.GetSection(nameof(AppConfig)));
            services.AddScoped<IIdentityService, IdentityService>();
            services.AddScoped<JwtService>();
            services.AddScoped<IEmailService, EmailService>();

            services.RegisterJobConsumers();

            return services;
        }
    }
}
