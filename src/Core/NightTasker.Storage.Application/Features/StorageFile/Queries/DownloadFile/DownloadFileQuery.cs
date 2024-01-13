using MediatR;
using NightTasker.Storage.Application.Features.StorageFile.Dtos;

namespace NightTasker.Storage.Application.Features.StorageFile.Queries.DownloadFile;

/// <summary>
/// Запрос на загрузку файла.
/// </summary>
/// <param name="BucketName">Имя хранилища.</param>
/// <param name="FileName">Имя файла.</param>
public record DownloadFileQuery(string BucketName, string FileName) : IRequest<DownloadedFileDto>;