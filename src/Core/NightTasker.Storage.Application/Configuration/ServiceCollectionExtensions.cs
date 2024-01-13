using Microsoft.Extensions.DependencyInjection;
using NightTasker.Storage.Application.ApplicationContracts.FileStorage;

namespace NightTasker.Storage.Application.Configuration;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection RegisterApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(typeof(ServiceCollectionExtensions).Assembly);
        });
        
        return services;
    }
}