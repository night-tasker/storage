using MapsterMapper;
using MediatR;
using NightTasker.Storage.Application.ApplicationContracts.FileStorage;
using NightTasker.Storage.Application.Features.StorageFile.Dtos;

namespace NightTasker.Storage.Application.Features.StorageFile.Queries.DownloadFile;

/// <summary>
/// Хэндлер запроса <see cref="DownloadFileQuery"/>.
/// </summary>
public class DownloadFileQueryHandler(
    IStorageService storageService, 
    IMapper mapper) : IRequestHandler<DownloadFileQuery, DownloadedFileDto>
{
    private readonly IStorageService _storageService = storageService ?? throw new ArgumentNullException(nameof(storageService));
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    
    public async Task<DownloadedFileDto> Handle(DownloadFileQuery request, CancellationToken cancellationToken)
    {
        var result = await _storageService.DownloadFile(request.BucketName, request.FileName);
        var downloadedFileDto = new DownloadedFileDto(result.Stream, result.ContentType);
        return downloadedFileDto;
    }
}