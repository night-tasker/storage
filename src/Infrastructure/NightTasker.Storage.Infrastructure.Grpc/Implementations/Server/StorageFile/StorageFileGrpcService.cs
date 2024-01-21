using Grpc.Core;
using MapsterMapper;
using MediatR;
using NightTasker.Common.Grpc.StorageFiles;
using NightTasker.Storage.Application.Features.StorageFile.Commands.RemoveFile;
using NightTasker.Storage.Application.Features.StorageFile.Commands.UploadFile;
using NightTasker.Storage.Application.Features.StorageFile.Dtos;
using NightTasker.Storage.Application.Features.StorageFile.Queries.DownloadFile;
using NightTasker.Storage.Application.Features.StorageFile.Queries.GetFilesUrl;
using NightTasker.Storage.Application.Features.StorageFile.Queries.GetFileUrl;
using NightTasker.Storage.Application.Models.StorageFile;

namespace NightTasker.Storage.Infrastructure.Grpc.Implementations.Server.StorageFile;

/// <summary>
/// gRPC-Сервис для работы с файлами.
/// </summary>
/// <param name="mediator"></param>
/// <param name="mapper"></param>
public class StorageFileGrpcService(
    IMediator mediator,
    IMapper mapper) : Common.Grpc.StorageFiles.StorageFile.StorageFileBase
{
    private readonly IMediator _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    
    /// <summary>
    /// Скачать файл.
    /// </summary>
    /// <param name="request">Запрос.</param>
    /// <param name="serverCallContext">Конекст запроса.</param>
    public override async Task<DownloadFileResponse> DownloadFile(
        DownloadFileRequest request, 
        ServerCallContext serverCallContext)
    {
        var query = _mapper.Map<DownloadFileQuery>(request);
        var result = await _mediator.Send(query, serverCallContext.CancellationToken);
        var bytes = result.Stream.ToArray();
        var response = new DownloadFileResponse
        {
            Data = Google.Protobuf.ByteString.CopyFrom(bytes)
        };

        return response;
    }

    /// <summary>
    /// Загрузить файл.
    /// </summary>
    /// <param name="request">Запрос.</param>
    /// <param name="context">Контекст запроса.</param>
    /// <returns>Результат запроса (название созданного файла).</returns>
    public override async Task<UploadFileResponse> UploadFile(UploadFileRequest request, ServerCallContext context)
    { 
        var stream = new MemoryStream(request.Data.ToByteArray());
        var uploadFileDto = new UploadFileDto(
            FileName: request.FileName,
            Stream: stream,
            ContentType: request.ContentType,
            Length: request.Data.Length,
            BucketName: request.BucketName);
        
        var uploadFilesDtoArray = new[] { uploadFileDto };
        var command = new UploadFilesCommand(uploadFilesDtoArray);
        var uploadedFiles = await _mediator.Send(command, context.CancellationToken);
        var uploadFileResponse = new UploadFileResponse
        {
            FileName = uploadedFiles.FirstOrDefault()
        };
        
        return uploadFileResponse;
    }
    
    /// <summary>
    /// Получить ссылку на файл.
    /// </summary>
    /// <param name="request">Запрос на получение ссылки.</param>
    /// <param name="context">Конекст запроса.</param>
    /// <returns>Ответ с ссылкой на файл.</returns>
    public override async Task<GetFileUrlResponse> GetFileUrl(GetFileUrlRequest request, ServerCallContext context)
    {
        var query = new GetFileUrlQuery(request.BucketName, request.FileName);
        var result = await _mediator.Send(query, context.CancellationToken);
        var response = new GetFileUrlResponse
        {
            Url = result
        };
        
        return response;
    }

    /// <summary>
    /// Получить ссылки на файлы.
    /// </summary>
    /// <param name="request">Запрос на получение ссылок на файлы.</param>
    /// <param name="context">Конекст запроса.</param>
    /// <returns>Ответ с ссылками на файлы.</returns>
    public override async Task<GetFilesUrlResponse> GetFilesUrl(GetFilesUrlRequest request, ServerCallContext context)
    {
        var lookingForFiles = request.Files
            .Select(x => new GetFileDto(x.BucketName, x.FileName))
            .ToList();
        
        var query = new GetFilesUrlQuery(lookingForFiles);
        var result = await _mediator.Send(query, context.CancellationToken);
        var response = new GetFilesUrlResponse();
        var responseFiles = _mapper.Map<IReadOnlyCollection<FileUrlDto>>(result);
        response.Files.AddRange(responseFiles);
        
        return response;
    }

    /// <summary>
    /// Удалить файл.
    /// </summary>
    /// <param name="request">Запрос на удаление файла.</param>
    /// <param name="context">Конекст запроса.</param>
    /// <returns>Результат удаления файла.</returns>
    public override async Task<RemoveFileResponse> RemoveFile(RemoveFileRequest request, ServerCallContext context)
    {
        var query = new RemoveFileCommand(request.BucketName, request.FileName);
        await _mediator.Send(query, context.CancellationToken);
        var response = new RemoveFileResponse()
        {
            Success = true
        };

        return response;
    }
}