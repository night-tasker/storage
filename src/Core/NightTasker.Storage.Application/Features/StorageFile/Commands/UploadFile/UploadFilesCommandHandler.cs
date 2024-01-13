using MediatR;
using NightTasker.Storage.Application.ApplicationContracts.FileStorage;
using NightTasker.Storage.Application.Features.StorageFile.Dtos;

namespace NightTasker.Storage.Application.Features.StorageFile.Commands.UploadFile;

/// <summary>
/// Хэндлер запроса <see cref="UploadFilesCommand"/>.
/// </summary>
public class UploadFilesCommandHandler(IStorageService storageService) : IRequestHandler<UploadFilesCommand>
{
    private readonly IStorageService _storageService =
        storageService ?? throw new ArgumentNullException(nameof(storageService));

    public Task Handle(UploadFilesCommand request, CancellationToken cancellationToken)
    {
        var files = request.Files;
        return _storageService.UploadFiles(files, cancellationToken);
    }
}