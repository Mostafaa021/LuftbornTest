
using Asp.Versioning;
using FluentValidation;
using Luftborn.Application.Common.Configurations;
using Luftborn.Application.Common.Factories;
using Luftborn.Application.Common.Validation;
using Luftborn.Application.Features.Product.Command;
using Luftborn.Application.Features.Product.Query;
using Luftborn.Application.Features.Product.Validators;
using Luftborn.Core.Abstraction.Domain;
using Luftborn.Infrastructure.Presistance.Data.Context;
using Luftborn.Infrastructure.Presistance.Data.Triggers;
using Luftborn.Infrastructure.Presistance.Data.UnitOfWorks;
using LuftbornTestApp.DataInitIalizer;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;

namespace LuftbornTestApp;

public static class ConfigureServices
{
    public static void RegisterServices(this WebApplicationBuilder builder, IConfigurationRoot configuration)
    {
        #region Configuration
        builder.Services.Configure<AppSettings>(builder.Configuration);
        #endregion

        #region Controllers
        builder.Services.AddControllers()
            .AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                options.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
            });
        #endregion

        #region Problem Details
        builder.Services.AddProblemDetails();
        #endregion

        #region API Versioning
        // builder.Services.AddApiVersioning(opt =>
        // {
        //     opt.DefaultApiVersion = new ApiVersion(1, 0);
        //     opt.AssumeDefaultVersionWhenUnspecified = true;
        //     opt.ReportApiVersions = true;
        // });
        #endregion

        #region Authentication
        builder.Services
            .AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
            });
        #endregion

        #region Authorization
        builder.Services.AddAuthorizationBuilder()
            .AddPolicy("WebUser", policy => policy.RequireAssertion(
                context => context.User.IsInRole("Contributor") 
                           || context.User.IsInRole("Admin")
                           || context.User.IsInRole("SuperAdmin")
            ))
            .AddPolicy("WebAdmin", policy => policy.RequireAssertion(
                context => context.User.IsInRole("Admin")
                           || context.User.IsInRole("SuperAdmin")
            ))
            .AddPolicy("SuperAdmin", policy => policy.RequireRole("SuperAdmin"));
        #endregion

        #region Database Context
        builder.Services.AddDbContext<ECommerceDbContext>(
            (_, optionsBuilder) =>
            {
                optionsBuilder.UseSqlServer(configuration.GetConnectionString("EcommerceDbConnection"))
                    .EnableSensitiveDataLogging()
                    .EnableDetailedErrors()
                    .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

                optionsBuilder.UseTriggers(triggerOptions =>
                {
                    triggerOptions.AddTrigger<ProductTrigger>();
                });
            });
        #endregion
        
        builder.Services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(typeof(GetAllActiveProductsHandler).Assembly));
        builder.Services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(typeof(GetProductsByCategoryHandler).Assembly));
        builder.Services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(typeof(GetProductByIdQueryHandler).Assembly));
        builder.Services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(typeof(CreateProductCommandHandler).Assembly));
        builder.Services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(typeof(UpdateExisitingProductHandler).Assembly));
        builder.Services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(typeof(DeleteExisitingProductCommandHandler).Assembly));
        
        // Register FluentValidation validators
        builder.Services.AddValidatorsFromAssemblyContaining<CreateProductCommandValidator>();
        // Register validation behavior in MediatR pipeline
        builder.Services.AddTransient(
            typeof(IPipelineBehavior<,>),
            typeof(RequestValidationBehavior<,>)
        );
        #region Mapping
        builder.Services.AddMapster(); 
        #endregion

        #region Application Services
        builder.Services.AddScoped<IECommerceUnitOfWork, ECommeceUnitOfWork>();
        builder.Services.AddSingleton<ProductFactoryProvider>();
        builder.Services.AddScoped<IProductFilterStrategyFactory, ProductFilterStrategyFactory>();
        builder.Services.AddHostedService<DataInitializationHostedService>();
        #endregion

        #region Swagger
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        #endregion
    }
}
