namespace NightTasker.Storage.Presentation.Requests.StorageFile;

/// <summary>
/// Запрос на загрузку файла.
/// </summary>
/// <param name="BucketName">Имя хранилища.</param>
/// <param name="FileName">Имя файла.</param>
public record DownloadFileRequest(string BucketName, string FileName);