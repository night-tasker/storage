using MediatR;

namespace NightTasker.Storage.Application.Features.StorageFile.Queries.GetFileUrl;

/// <summary>
/// Получить ссылку на файл.
/// </summary>
/// <param name="BucketName">Имя хранилища.</param>
/// <param name="FileName">Имя файла.</param>
public record GetFileUrlQuery(string BucketName, string FileName) : IRequest<string>;