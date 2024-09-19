namespace APis.Extensions
{
    public static class APIBootstrapper
    {
        public static IServiceCollection AddApi(this IServiceCollection services)
        {
            services.AddSwaggerExtension().AddApiVersioningExtension();

            return services;
        }
    }
}
