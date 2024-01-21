using MediatR;
using NightTasker.Storage.Application.ApplicationContracts.FileStorage;

namespace NightTasker.Storage.Application.Features.StorageFile.Queries.GetFileUrl;

/// <summary>
/// Хэндлер для запроса <see cref="GetFileUrlQuery"/>.
/// </summary>
public class GetFileUrlQueryHandler(IStorageService storageService) : IRequestHandler<GetFileUrlQuery, string>
{
    private readonly IStorageService _storageService = storageService ?? throw new ArgumentNullException(nameof(storageService));

    public async Task<string> Handle(GetFileUrlQuery request, CancellationToken cancellationToken)
    {
        var result = await _storageService.GetFileUrl(request.BucketName, request.FileName, cancellationToken);
        return result;
    }
}