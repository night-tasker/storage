using Microsoft.Extensions.Options;
using Minio;
using Minio.DataModel.Args;
using Minio.Exceptions;
using NightTasker.Storage.Application.Exceptions.NotFound;
using NightTasker.Storage.Application.Features.StorageFile.Dtos;
using NightTasker.Storage.Application.Models.StorageFile;
using NightTasker.Storage.Infrastructure.FileStorage.Settings;

namespace NightTasker.Storage.Infrastructure.FileStorage.Implementations;

/// <inheritdoc />
public class StorageService(
    IMinioClient minioClient,
    IOptions<MinioSettings> minioSettings) : Application.ApplicationContracts.FileStorage.IStorageService
{
    private readonly IMinioClient _minioClient = minioClient ?? throw new ArgumentNullException(nameof(minioClient));
    private readonly MinioSettings _minioSettings = minioSettings.Value ?? throw new ArgumentNullException(nameof(minioSettings));

    /// <inheritdoc />
    public async Task<DownloadFileResult> DownloadFile(string bucketName, string fileName)
    {
        const int initialBufferSize = 256 * 1024;
        var memoryStream = new MemoryStream(capacity: initialBufferSize);
        var objectArgs = new GetObjectArgs()
            .WithObject(fileName)
            .WithBucket(bucketName)
            .WithCallbackStream(async (minioStream, token) =>
            {
                const int bufferSize = 64 * 1024;
                await minioStream.CopyToAsync(memoryStream, bufferSize, token);
            });

        try
        {
            var objectStat = await _minioClient.GetObjectAsync(objectArgs);
            memoryStream.Seek(offset: 0, SeekOrigin.Begin);
            var result = new DownloadFileResult(memoryStream, objectStat.ContentType);
            return result;
        }
        catch (BucketNotFoundException)
        {
            throw new StorageFileNotFoundException(bucketName, fileName);
        }
        catch (ObjectNotFoundException)
        {
            throw new StorageFileNotFoundException(bucketName, fileName);
        }
    }

    /// <inheritdoc />
    public async Task<IReadOnlyCollection<string>> UploadFiles(
        IReadOnlyCollection<UploadFileDto> files,
        CancellationToken cancellationToken)
    {
        foreach (var file in files)
        {
            await EnsureBucketExists(file.BucketName);
            var objectArgs = new PutObjectArgs()
                .WithBucket(file.BucketName)
                .WithObject(file.FileName)
                .WithContentType(file.ContentType)
                .WithStreamData(file.Stream)
                .WithObjectSize(file.Length);
            await _minioClient.PutObjectAsync(objectArgs, cancellationToken);
        }
        
        return files.Select(x => x.FileName).ToList();
    }

    /// <inheritdoc />
    public async Task<string> GetFileUrl(string bucketName, string fileName, CancellationToken cancellationToken)
    {
        try
        {
            var preSignedGetObjectArgs = new PresignedGetObjectArgs()
                .WithBucket(bucketName)
                .WithObject(fileName)
                .WithExpiry(_minioSettings.PreSignedObjectsExpiryRange);
        
            var url = await _minioClient.PresignedGetObjectAsync(preSignedGetObjectArgs);
            return url;
        }
        catch (BucketNotFoundException)
        {
            throw new StorageFileNotFoundException(bucketName, fileName);
        }
        catch (ObjectNotFoundException)
        {
            throw new StorageFileNotFoundException(bucketName, fileName);
        }
    }

    /// <inheritdoc />
    public async Task<IReadOnlyCollection<FileWithUrlDto>> GetFilesUrl(
        IReadOnlyCollection<GetFileDto> files, CancellationToken cancellationToken)
    {
        var result = new List<FileWithUrlDto>();
        
        foreach (var file in files)
        {
            var url = await GetFileUrl(file.BucketName, file.FileName, cancellationToken);
            var dto = new FileWithUrlDto(file.FileName, file.BucketName, url);
            result.Add(dto);
        }

        return result;
    }

    /// <inheritdoc />
    public Task RemoveFile(string bucketName, string fileName, CancellationToken cancellationToken)
    {
        var removeObjectArgs = new RemoveObjectArgs()
            .WithBucket(bucketName)
            .WithObject(fileName);
        
        return _minioClient.RemoveObjectAsync(removeObjectArgs, cancellationToken);
    }

    /// <summary>
    /// Убедиться, что хранилище существует.
    /// </summary>
    /// <param name="bucketName">Название хранилища.</param>
    private async Task EnsureBucketExists(string bucketName)
    {
        var bucketExistsArgs = new BucketExistsArgs()
            .WithBucket(bucketName);
        var bucketExists = await _minioClient.BucketExistsAsync(bucketExistsArgs);
        if (!bucketExists)
        {
            var makeBucketArgs = new MakeBucketArgs()
                .WithBucket(bucketName);
            await _minioClient.MakeBucketAsync(makeBucketArgs);
        }
    }
}