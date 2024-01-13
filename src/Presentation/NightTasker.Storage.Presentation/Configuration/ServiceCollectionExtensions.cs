using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;
using Mapster;
using MapsterMapper;
using Microsoft.OpenApi.Models;
using NightTasker.Common.Core.Identity.Contracts;
using NightTasker.Common.Core.Identity.Implementations;
using NightTasker.Storage.Infrastructure.Persistence.Contracts;
using NightTasker.Storage.Presentation.Constants;
using NightTasker.Storage.Presentation.Implementations;

namespace NightTasker.Storage.Presentation.Configuration;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection RegisterApiServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddMapper();
        services.AddHttpContextAccessor();
        services.AddScoped<IIdentityService, IdentityService>();
        services.AddScoped<IApplicationDbAccessor, ApplicationDbAccessor>();
        return services;
    }
    
    /// <summary>
    /// Добавить Маппер.
    /// </summary>
    /// <param name="services">Коллекция сервисов.</param>
    private static IServiceCollection AddMapper(this IServiceCollection services)
    {
        var typeAdapterConfig = TypeAdapterConfig.GlobalSettings;
        typeAdapterConfig.Scan(typeof(ServiceCollectionExtensions).Assembly);
        typeAdapterConfig.RequireExplicitMapping = true;
        var mapperConfig = new Mapper(typeAdapterConfig);
        services.AddSingleton<IMapper>(mapperConfig);
        return services;
    }
    
    /// <summary>
    /// Добавить Swagger.
    /// </summary>
    /// <param name="services">Коллекция сервисов.</param>
    public static void AddSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo 
                { Title = "night-tasker-storage", Version = "1.0", Description = "night tasker storage application" }
            );
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            options.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);
        });
    }

    /// <summary>
    /// Добавить валидацию.
    /// </summary>
    /// <param name="services">Коллекция сервисов.</param>
    public static void AddValidation(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<Program>();
        services.AddFluentValidationAutoValidation(configuration =>
        {
            configuration.DisableDataAnnotationsValidation = true;
        });
    }

    /// <summary>
    /// Добавить дефолтные настройки CORS.
    /// </summary>
    /// <param name="services">Коллекция сервисов.</param>
    public static IServiceCollection AddDefaultCorsPolicy(this IServiceCollection services)
    {
        services.AddCors(policy =>
        {
            policy.AddPolicy(CorsConstants.DefaultCorsPolicyName, builder => builder
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod());
        });
        
        return services;
    }
}