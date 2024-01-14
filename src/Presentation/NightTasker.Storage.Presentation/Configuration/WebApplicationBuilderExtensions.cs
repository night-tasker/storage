using System.Net;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using NightTasker.Storage.Presentation.Settings;

namespace NightTasker.Storage.Presentation.Configuration;

public static class WebApplicationBuilderExtensions
{
    public static void ConfigureHttp1AndHttp2(
        this WebApplicationBuilder builder)
    {
        var configuration = builder.Configuration;
        var httpSettingsSection = configuration.GetSection(nameof(HttpSettings));
        var httpSettings = httpSettingsSection.Get<HttpSettings>()!;
        builder.Services.Configure<HttpSettings>(httpSettingsSection);
        
        builder.WebHost.ConfigureKestrel(options =>
        {
            options.Listen(IPAddress.Any, int.Parse(httpSettings.Http1ListeningPort!), listenOptions =>
            {
                listenOptions.Protocols = HttpProtocols.Http1;
            });

            options.Listen(IPAddress.Any, int.Parse(httpSettings.Http2ListeningPort!), listenOptions =>
            {
                listenOptions.Protocols = HttpProtocols.Http2;
            });
        });
    }
}