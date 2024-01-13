namespace NightTasker.Storage.Infrastructure.FileStorage.Settings;

/// <summary>
/// Настройки подключения к Minio.
/// </summary>
/// <param name="Endpoint">Адрес эндпоинта.</param>
/// <param name="AccessKey">Ключ доступа.</param>
/// <param name="SecretKey">Ключ-секрет.</param>
public record MinioSettings(string Endpoint, string AccessKey, string SecretKey);