using Core.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.DI
{
    public static class RepositoriesBootstrapper
    {
        public static IServiceCollection RegisterRepositories(this IServiceCollection services)
        {
            services.AddScoped<ICartItemRepository, CartItemRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<ICartRepository, CartRepository>();
            services.AddScoped<ICustomCanvasRepository, CustomCanvasRepository>();
            services.AddScoped<IOrderItemRepository, OrderItemRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IProviderRateRepository, ProviderRateRepository>();
            services.AddScoped<IProviderRepository, ProviderRepository>();
            services.AddScoped<ITemplateCanvasRepository, TemplateCanvasRepository>();
            services.AddScoped<IVoucherRepository, VoucherRepository>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();
            services.AddScoped<ISuggestionCanvasRepository, SuggestionCanvasRepository>();

            return services;
        }
    }
}