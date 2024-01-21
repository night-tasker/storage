using MediatR;
using NightTasker.Storage.Application.ApplicationContracts.FileStorage;
using NightTasker.Storage.Application.Models.StorageFile;

namespace NightTasker.Storage.Application.Features.StorageFile.Queries.GetFilesUrl;

/// <summary>
/// Хэндлер для запроса <see cref="GetFilesUrlQuery"/>.
/// </summary>
public class GetFilesUrlQueryHandler(IStorageService storageService)
    : IRequestHandler<GetFilesUrlQuery, IReadOnlyCollection<FileWithUrlDto>>
{
    private readonly IStorageService _storageService = storageService ?? throw new ArgumentNullException(nameof(storageService));

    public Task<IReadOnlyCollection<FileWithUrlDto>> Handle(GetFilesUrlQuery request, CancellationToken cancellationToken)
    {
        return _storageService.GetFilesUrl(request.Files, cancellationToken);
    }
}