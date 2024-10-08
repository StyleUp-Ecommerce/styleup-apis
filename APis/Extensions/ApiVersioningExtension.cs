﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;

namespace APis.Extensions
{
    public static class ApiVersioningExtension
    {
        public static IServiceCollection AddApiVersioningExtension(this IServiceCollection services)
        {
            services.AddApiVersioning(options =>
            {
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.ReportApiVersions = true;
                options.ApiVersionReader = ApiVersionReader.Combine(
                    new QueryStringApiVersionReader("version")
                );
            });

            //services.AddVersionedApiExplorer(options =>
            //{
            //    options.GroupNameFormat = "'v'VW";
            //    options.SubstituteApiVersionInUrl = true;
            //});

            return services;
        }
    }
}
