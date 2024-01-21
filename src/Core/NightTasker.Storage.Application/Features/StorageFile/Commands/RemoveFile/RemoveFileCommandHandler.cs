using MediatR;
using NightTasker.Storage.Application.ApplicationContracts.FileStorage;

namespace NightTasker.Storage.Application.Features.StorageFile.Commands.RemoveFile;

/// <summary>
/// Хэндлер для <see cref="RemoveFileCommand"/>
/// </summary>
internal class RemoveFileCommandHandler(IStorageService storageService) : IRequestHandler<RemoveFileCommand>
{
    private readonly IStorageService _storageService = 
        storageService ?? throw new ArgumentNullException(nameof(storageService));

    public Task Handle(RemoveFileCommand request, CancellationToken cancellationToken)
    {
        return _storageService.RemoveFile(request.BucketName, request.FileName, cancellationToken);
    }
}