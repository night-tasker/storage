#pragma warning disable CA2255
using System.Runtime.CompilerServices;

namespace NightTasker.Storage.Infrastructure.Persistence.Configuration;

/// <summary>
/// Инициализатор модуля.
/// </summary>
public static class ModuleInitializer
{
    [ModuleInitializer]
    public static void Initialize()
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }
}

