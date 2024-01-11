using NightTasker.Storage.Application.Configuration;
using NightTasker.Storage.Infrastructure.Messaging.Configuration;
using NightTasker.Storage.Infrastructure.Persistence.Configuration;
using NightTasker.Storage.Presentation.Configuration;
using NightTasker.Storage.Presentation.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpc();

builder.Services
    .RegisterApplicationServices()
    .RegisterPersistenceServices(builder.Configuration)
    .RegisterMessagingServices(builder.Configuration)
    .RegisterApiServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<GreeterService>();
app.MapGet("/",
    () =>
        "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();