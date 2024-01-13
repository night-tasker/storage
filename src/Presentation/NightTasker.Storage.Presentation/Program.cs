using NightTasker.Common.Core.Exceptions.Middlewares;
using NightTasker.Storage.Application.Configuration;
using NightTasker.Storage.Infrastructure.FileStorage.Configuration;
using NightTasker.Storage.Infrastructure.Messaging.Configuration;
using NightTasker.Storage.Infrastructure.Persistence.Configuration;
using NightTasker.Storage.Presentation.Configuration;
using NightTasker.Storage.Presentation.Constants;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));

builder.Services
    .RegisterApplicationServices()
    .RegisterPersistenceServices(builder.Configuration)
    .RegisterMessagingServices(builder.Configuration)
    .RegisterFileStorageServices(builder.Configuration)
    .RegisterApiServices(builder.Configuration);

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddSwagger();
}

builder.Services.AddValidation();

builder.Services.AddDefaultCorsPolicy();

var app = builder.Build();

await app.ApplyDatabaseMigrationsAsync(CancellationToken.None);

app.UseSerilogRequestLogging();

if (builder.Environment.IsDevelopment())
{
    app.ConfigureSwagger(); 
}

app.UseMiddleware<ExceptionHandlerMiddleware>();

app.UseCors(CorsConstants.DefaultCorsPolicyName);

app.UseAuthorization();

app.MapControllers();

app.Run();