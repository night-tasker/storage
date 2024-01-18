namespace NightTasker.Storage.Infrastructure.FileStorage.Settings;

/// <summary>
/// Настройки подключения к Minio.
/// </summary>
public class MinioSettings
{
    /// <summary>Адрес эндпоинта.</summary>
    public string? Endpoint { get; set; }

    /// <summary>Ключ доступа.</summary>
    public string? AccessKey { get; set; }

    /// <summary>Ключ-секрет.</summary>
    public string? SecretKey { get; set; }

    /// <summary>Время жизни ссылок.</summary>
    public int PreSignedObjectsExpiryRange { get; set; }
}