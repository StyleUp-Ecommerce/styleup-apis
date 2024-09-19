using CleanBase.Core.Api;
using CleanBase.Core.Data.UnitOfWorks;
using CleanBase.Core.Infrastructure.Jobs.Hosting;
using CleanBase.Core.Services.Jobs;
using Domain.Validators;
using Domain;
using Core.ViewModels.Profiles;
using Core;
using Infrastructure;
using Infrastructure.UnitOfWorks;
using Infrastructure.Azure.KeyVault;
using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Authorization;
using CleanBase.Core.Api.Swagger;
using CleanBase.Core.Security;
using CleanBase.Core.Services.Core.Base;
using CleanBase.Core.Api.Authorization;
using Infrastructure.Jobs;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using CleanBase.Core.Services.Core;
using CleanBase.Core.Data.Policies.Base;
using CleanBase.Core.Infrastructure.Policies;
using CleanBase.Core.Services.Storage;
using CleanBase.Core.Api.Middlewares;
using CleanBase.Core.Api.Extensions;
using APis.Extensions;
using Infrastructure.Extensions;

// Create builder
var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

// Configure logging
Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateLogger();

builder.Services.AddSingleton(Log.Logger);

builder
    .Configuration.SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
    .AddEnvironmentVariables();


// Configure configuration sources
var vaultName = config["KeyVault:VaultName"];

// Uncomment if Key Vault is used
// builder.Configuration.AddAzureKeyVault(
//     $"https://{vaultName}.vault.azure.net/",
//     config["KeyVault:ClientId"],
//     config["KeyVault:ClientSecret"],
//     new PrefixKeyVaultManager(config["KeyVault:Prefix"])
// );

// Add services to the container
builder.Services.AddControllers(options =>
{
    options.Filters.Add(new AuthorizeFilter(new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build()));
});
builder.Services.AddEndpointsApiExplorer();

// Configure authentication and authorization
builder.Services.AddSingleton<IAuthorizationHandler, RolePermissionHandler>();
builder.Services.AddSingleton<IAuthorizationPolicyProvider, AuthorizationPolicyProvider>();
builder.Services.AddIdentityExtension(config).AddIdentityServerExtension(config);


// Add Hangfire
var hangfireOptions = new PostgreSqlStorageOptions
{
    QueuePollInterval = TimeSpan.FromSeconds(5),
    PrepareSchemaIfNecessary = true,
    SchemaName = "public"
};
var storage = new PostgreSqlStorage(builder.Configuration.GetConnectionString("DefaultConnection"), hangfireOptions);
builder.Services.AddHangfire(config => config.UseStorage(storage));
builder.Services.AddHangfireServer(options => options.WorkerCount = 5 * Environment.ProcessorCount);


// Register additional services
builder.Services.AddScoped<ISmartLogger, SmartLogger>();
builder.Services.AddScoped<IIdentityProvider, IdentityProvider>();
builder.Services.AddScoped<IPolicyFactory, PolicyFactory>();
builder.Services.AddScoped<ICoreProvider, CoreProvider>();
builder.Services.AddScoped(typeof(IStorageService<>), typeof(StorageService<>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWorkGeneral>();
builder.Services.AddAutoMapperProfiles();
builder.Services.RegisterValidators();
builder.Services.AddApi().RegisterInfrastructureService(builder.Configuration).RegisterDomainService();

// Build the app
var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwaggerExtension();
}

// Init middleware
app.UseMiddleware<HttpInjectMiddleware>(Array.Empty<object>());
app.UseMiddleware<RequestLoggingMiddleware>(Array.Empty<object>());

app.UseExceptionConfiguration();
app.UseHttpsRedirection();
app.UseAuthorization();
app.UseCors(policy => policy
    // .WithOrigins(config["Cors:AllowedHosts"]?.Split(',') ?? new[] { "*" })
    .AllowAnyOrigin()
    .AllowAnyHeader()
    .AllowAnyMethod());

app.MapControllers();


app.UseHangfireDashboard("/hangfire", new DashboardOptions
{
    Authorization = new[] { new DashboardAuthorizationFilter() }
});

// Seed data
//using (var scope = app.Services.CreateScope())
//{
//    var contextSeed = scope.ServiceProvider.GetRequiredService<ContextSeed>();
//    await contextSeed.SeedData();
//}

app.Run();
