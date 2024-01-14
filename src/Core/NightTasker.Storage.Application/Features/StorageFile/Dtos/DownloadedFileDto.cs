namespace NightTasker.Storage.Application.Features.StorageFile.Dtos;

/// <summary>
/// DTO загруженного файла.
/// </summary>
/// <param name="Stream">Поток.</param>
/// <param name="ContentType">Тип файла.</param>
public record DownloadedFileDto(MemoryStream Stream, string ContentType);