using System.Text;
using Mapster;
using MapsterMapper;
using NightTasker.Common.Core.Identity.Contracts;
using NightTasker.Common.Core.Identity.Implementations;
using NightTasker.Storage.Infrastructure.Persistence.Contracts;
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
        typeAdapterConfig.Scan(
            typeof(ServiceCollectionExtensions).Assembly,
            typeof(Application.Configuration.ServiceCollectionExtensions).Assembly);
        typeAdapterConfig.RequireExplicitMapping = true;
        var mapperConfig = new Mapper(typeAdapterConfig);
        services.AddSingleton<IMapper>(mapperConfig);
        return services;
    }
}