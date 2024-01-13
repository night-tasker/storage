namespace NightTasker.Storage.Presentation.Requests.StorageFile;

/// <summary>
/// Запрос на загрузку файла.
/// </summary>
/// <param name="File">Файл.</param>
/// <param name="BasketName">Имя хранилища.</param>
public record UploadFileRequest(IFormFile File, string BasketName, string FileName);