using Microsoft.EntityFrameworkCore;
using NightTasker.Storage.Infrastructure.Grpc.Implementations.Server.StorageFile;
using NightTasker.Storage.Infrastructure.Persistence;
using NightTasker.Storage.Infrastructure.Persistence.Configuration;

namespace NightTasker.Storage.Presentation.Configuration;

/// <summary>
/// Класс для методов расширения для WebApplication.
/// </summary>
public static class WebApplicationExtensions
{
    /// <summary>
    /// Конфигурация Swagger
    /// </summary>
    /// <param name="app">WebApplication</param>
    public static void ConfigureSwagger(this WebApplication app)
    {
        app.UseSwagger()
            .UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            });
    }

    /// <summary>
    /// Применение миграций базы данных.
    /// </summary>
    /// <param name="app">WebApplication.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    public static async Task ApplyDatabaseMigrationsAsync(
        this WebApplication app,
        CancellationToken cancellationToken)
    {
        await using var scope = app.Services.CreateAsyncScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        if ((await context.Database.GetPendingMigrationsAsync(cancellationToken)).Any())
        {
            await context.MigrateAsync(cancellationToken);
        }
    }

    public static void MapGrpcServices(this WebApplication app)
    {
        app.MapGrpcService<StorageFileGrpcService>();
    }
}