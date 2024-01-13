namespace NightTasker.Storage.Presentation.Endpoints;

/// <summary>
/// Пути эндпоинтов для работы с файлами в хранилище.
/// </summary>
public static class StorageFileEndpoints
{
    /// <summary>
    /// Путь до эндпоинтов для работы с файлами в хранилище (основной).
    /// </summary>
    public const string StorageFileResource = "storage-files";
    
    /// <summary>
    /// Путь до эндпоинта для скачивания файла.
    /// </summary>
    public const string DownloadFile = "download";

    /// <summary>
    /// Путь до эндпоинта для загрузки файла.
    /// </summary>
    public const string UploadFile = "upload";
}