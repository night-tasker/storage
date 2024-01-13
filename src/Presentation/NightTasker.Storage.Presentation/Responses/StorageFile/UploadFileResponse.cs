namespace NightTasker.Storage.Presentation.Responses.StorageFile;

/// <summary>
/// Ответ на запрос на загрузку файла.
/// </summary>
/// <param name="FileId">ИД загруженного файла.</param>
public class UploadFileResponse(Guid FileId);