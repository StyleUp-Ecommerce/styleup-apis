using Core.Identity.Email.Interfaces;
using Core.Identity.Interfaces;
using Core.Services;
using Domain.Identity.Email;
using Domain.Identity.Providers;
using Domain.Services;
using Domain.Strategies;
using Microsoft.Extensions.DependencyInjection;

namespace Domain
{
    public static class DomainServiceBootstrapper
    {
        public static IServiceCollection RegisterDomainService(this IServiceCollection services)
        {
            services.AddScoped<ICartService, CartService>();
            services.AddScoped<ICartItemService, CartItemService>();
            services.AddScoped<ICustomCanvasService, CustomCanvasService>();
            services.AddScoped<IProviderRateService, ProviderRateService>();
            services.AddScoped<IProviderService, ProviderService>();
            services.AddScoped<ITemplateCanvasService, TemplateCanvasService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IOrderItemService, OrderItemService>();
            services.AddScoped<IVoucherService, VoucherService>();
            services.AddScoped<ISuggestionCanvasService, SuggestionCanvasService>();

            services.AddSingleton(_ =>
            {
                return new List<IEmailClassifier>()
                {
                        new EmailVerificationTemplate(),
                        new ResetPasswordTemplate(),
                        new OrderedTemplate(),
                };

            });
            services.AddSingleton(_ =>
            {
                return new List<IExternalProvider>() { new GoogleProvider() };
            });

            services.RegisTrageries();
            return services;
        }
    }
}