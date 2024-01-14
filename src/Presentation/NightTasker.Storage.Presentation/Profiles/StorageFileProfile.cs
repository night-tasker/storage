using Mapster;
using NightTasker.Storage.Application.Features.StorageFile.Dtos;
using NightTasker.Storage.Application.Features.StorageFile.Queries.DownloadFile;
using NightTasker.Storage.Application.Models.StorageFile;
using NightTasker.Storage.Presentation.Requests.StorageFile;

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
    }
}