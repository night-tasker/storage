using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Minio;
using NightTasker.Storage.Application.ApplicationContracts.FileStorage;
using NightTasker.Storage.Infrastructure.FileStorage.Implementations;
using NightTasker.Storage.Infrastructure.FileStorage.Settings;

namespace NightTasker.Storage.Infrastructure.FileStorage.Configuration;

/// <summary>
/// Класс методов расширений для коллекции сервисов.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Регистрация служб для слоя <see cref="NightTasker.Storage.Infrastructure.FileStorage"/>.
    /// </summary>
    /// <param name="services">Коллекция сервисов.</param>
    /// <param name="configuration">Конфигурация.</param>
    /// <returns></returns>
    public static IServiceCollection RegisterFileStorageServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.ConfigureMinio(configuration);
        services.AddFileStorageServices();
        return services;
    }

    private static IServiceCollection AddFileStorageServices(this IServiceCollection services)
    {
        services.AddScoped<IStorageService, StorageService>();
        return services;
    }

    private static IServiceCollection ConfigureMinio(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var minioSettingsSection = configuration.GetSection(nameof(MinioSettings));
        services.Configure<MinioSettings>(minioSettingsSection);
        var minioSettings = minioSettingsSection.Get<MinioSettings>() ??
                            throw new ArgumentNullException(nameof(minioSettingsSection));

        services.AddMinio(configureClient => configureClient
            .WithEndpoint(minioSettings.Endpoint)
            .WithSSL(false)
            .WithCredentials(minioSettings.AccessKey, minioSettings.SecretKey));

        return services;
    }
}