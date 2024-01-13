using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NightTasker.Storage.Application.Features.StorageFile.Commands.UploadFile;
using NightTasker.Storage.Application.Features.StorageFile.Dtos;
using NightTasker.Storage.Application.Features.StorageFile.Queries.DownloadFile;
using NightTasker.Storage.Presentation.Constants;
using NightTasker.Storage.Presentation.Endpoints;
using NightTasker.Storage.Presentation.Requests.StorageFile;

namespace NightTasker.Storage.Presentation.Controllers;

/// <summary>
/// Контроллер для работы с файлами в хранилище.
/// </summary>
[ApiController]
[Route($"{ApiConstants.DefaultPrefix}/{ApiConstants.V1}/{StorageFileEndpoints.StorageFileResource}")]
public class StorageFileController(
    IMediator mediator,
    IMapper mapper) : ControllerBase
{
    private readonly IMediator _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

    /// <summary>
    /// Эндпоинт для скачивания файла.
    /// </summary>
    /// <param name="request">Запрос на скачивание файла.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Скачиваемый файл.</returns>
    [HttpPost(StorageFileEndpoints.DownloadFile)]
    public async Task<IActionResult> DownloadFile(
        [FromBody] DownloadFileRequest request,
        CancellationToken cancellationToken)
    {
        var query = _mapper.Map<DownloadFileQuery>(request);
        var result = await _mediator.Send(query, cancellationToken);
        return File(result.Stream, result.ContentType);
    }

    /// <summary>
    /// Эндпоинт для загрузки файлов.
    /// </summary>
    /// <param name="files">Запрос на загрузку файлов.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>ИД загруженных файлов.</returns>
    [HttpPost(StorageFileEndpoints.UploadFile)]
    public async Task<IActionResult> UploadFile(
        [FromForm] List<UploadFileRequest> files,
        CancellationToken cancellationToken)
    {
        var filesToUpload = _mapper.Map<IReadOnlyCollection<UploadFileDto>>(files);
        var query = new UploadFilesCommand(filesToUpload);
        await _mediator.Send(query, cancellationToken);
        return Ok();
    }
}