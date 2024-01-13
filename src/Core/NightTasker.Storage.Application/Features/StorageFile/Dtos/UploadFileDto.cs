using Microsoft.AspNetCore.Http;

namespace NightTasker.Storage.Application.Features.StorageFile.Dtos;

/// <summary>
/// DTO для загрузки файла.
/// </summary>
/// <param name="FileName">Файл.</param>
/// <param name="BucketName">Имя хранилища.</param>
/// <param name="Stream">Поток.</param>
/// <param name="ContentType">Тип файла.</param>
/// <param name="Length">Размер файла.</param>
public record UploadFileDto(string FileName, Stream Stream, string ContentType, long Length, string BucketName);