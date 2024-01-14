using MediatR;
using NightTasker.Storage.Application.Features.StorageFile.Dtos;

namespace NightTasker.Storage.Application.Features.StorageFile.Commands.UploadFile;

/// <summary>
/// Запрос на загрузку файла.
/// </summary>
/// <param name="Files">Файлы для загрузки.</param>
public record UploadFilesCommand(
    IReadOnlyCollection<UploadFileDto> Files) : IRequest<IReadOnlyCollection<string>>;