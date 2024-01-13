namespace NightTasker.Storage.Application.Models.StorageFile;

/// <summary>
/// Результат скачивания файла.
/// </summary>
/// <param name="Stream">Поток.</param>
/// <param name="ContentType">Тип содержимого.</param>
public record DownloadFileResult(MemoryStream Stream, string ContentType);