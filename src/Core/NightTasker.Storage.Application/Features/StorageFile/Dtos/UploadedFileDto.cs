namespace NightTasker.Storage.Application.Features.StorageFile.Dtos;

/// <summary>
/// DTO загруженного файла.
/// </summary>
/// <param name="UploadedFileName">Имя загруженного файла.</param>
/// <param name="UploadedFileId">ИД загруженного файла.</param>
public record UploadedFileDto(string UploadedFileName, Guid UploadedFileId);