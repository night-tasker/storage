namespace NightTasker.Storage.Application.Models.StorageFile;

/// <summary>
/// DTO для получения файла.
/// </summary>
/// <param name="BucketName">Имя хранилища.</param>
/// <param name="FileName">Имя файла.</param>
public record GetFileDto(string BucketName, string FileName);