namespace NightTasker.Storage.Application.Models.StorageFile;

/// <summary>
/// DTO для файла URL.
/// </summary>
/// <param name="FileName">Имя файла.</param>
/// <param name="BucketName">Имя хранилища.</param>
/// <param name="Url">URL файла.</param>
public record FileWithUrlDto(string FileName, string BucketName, string Url);