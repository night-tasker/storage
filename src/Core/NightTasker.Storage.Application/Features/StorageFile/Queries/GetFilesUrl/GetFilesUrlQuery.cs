using MediatR;
using NightTasker.Storage.Application.Models.StorageFile;

namespace NightTasker.Storage.Application.Features.StorageFile.Queries.GetFilesUrl;

/// <summary>
/// Получить ссылки на файлы.
/// </summary>
/// <param name="Files">Искомые файлы.</param>
public record GetFilesUrlQuery(IReadOnlyCollection<GetFileDto> Files) : IRequest<IReadOnlyCollection<FileWithUrlDto>>;