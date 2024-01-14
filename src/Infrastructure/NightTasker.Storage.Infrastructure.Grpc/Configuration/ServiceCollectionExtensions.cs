using Microsoft.Extensions.DependencyInjection;

namespace NightTasker.Storage.Infrastructure.Grpc.Configuration;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection RegisterGrpcServices(this IServiceCollection services)
    {
        services.AddGrpc();
        return services;
    }
}