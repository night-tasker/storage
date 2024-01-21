using Mapster;
using NightTasker.Common.Grpc.StorageFiles;
using NightTasker.Storage.Application.Features.StorageFile.Dtos;
using NightTasker.Storage.Application.Features.StorageFile.Queries.DownloadFile;
using NightTasker.Storage.Application.Models.StorageFile;
using DownloadFileRequest = NightTasker.Storage.Presentation.Requests.StorageFile.DownloadFileRequest;
using UploadFileRequest = NightTasker.Storage.Presentation.Requests.StorageFile.UploadFileRequest;

namespace NightTasker.Storage.Presentation.Profiles;

/// <summary>
/// Профиль для маппинга файлов.
/// </summary>
public class StorageFileProfile : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.ForType<DownloadFileRequest, DownloadFileQuery>();

        config.ForType<UploadFileRequest, UploadFileDto>()
            .ConstructUsing(src => new UploadFileDto(
                src.FileName,
                src.File.OpenReadStream(),
                src.File.ContentType,
                src.File.Length,
                src.BasketName));

        config.ForType<Common.Grpc.StorageFiles.DownloadFileRequest, DownloadFileQuery>();

        config.ForType<FileWithUrlDto, FileUrlDto>();
    }
}