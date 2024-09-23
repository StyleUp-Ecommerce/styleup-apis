using Core.Strategies.Discount;
using Domain.Strategies.Discount.Factory;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Strategies
{
    public static class StrategyBootstraper
    {
        public static IServiceCollection RegisTrageries(this IServiceCollection services)
        {
            services.AddTransient<IDiscountStrategyFactory, FixedAmountDiscountFactory>();
            services.AddTransient<IDiscountStrategyFactory, PercentageDiscountFactory>();
            services.AddTransient<IDiscountStrategyFactory, QuantityDiscountFactory>();

            return services;
        }
    }
}
