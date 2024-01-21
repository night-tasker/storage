using MediatR;

namespace NightTasker.Storage.Application.Features.StorageFile.Commands.RemoveFile;

/// <summary>
/// Команда для удаления файла.
/// </summary>
/// <param name="BucketName">Имя хранилища.</param>
/// <param name="FileName">Имя файла.</param>
public record RemoveFileCommand(string BucketName, string FileName) : IRequest;