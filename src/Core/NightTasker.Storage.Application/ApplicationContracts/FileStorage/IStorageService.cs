﻿using NightTasker.Storage.Application.Features.StorageFile.Dtos;
using NightTasker.Storage.Application.Models.StorageFile;

namespace NightTasker.Storage.Application.ApplicationContracts.FileStorage;

/// <summary>
/// Интерфейс службы хранилища файлов.
/// </summary>
public interface IStorageService
{
    /// <summary>
    /// Скачать файл.
    /// </summary>
    /// <param name="bucketName">Название хранилища.</param>
    /// <param name="fileName">Название файла.</param>
    /// <returns>Результат скачивания файла.</returns>
    Task<DownloadFileResult> DownloadFile(string bucketName, string fileName);

    /// <summary>
    /// Загрузить файлы.
    /// </summary>
    /// <param name="files">Файлы для загрузки.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    Task<IReadOnlyCollection<string>> UploadFiles(IReadOnlyCollection<UploadFileDto> files, CancellationToken cancellationToken);

    /// <summary>
    /// Получить ссылку на файл.
    /// </summary>
    /// <param name="bucketName">Имя хранилища.</param>
    /// <param name="fileName">Имя файла.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Ссылка на файл.</returns>
    Task<string> GetFileUrl(string bucketName, string fileName, CancellationToken cancellationToken);
    
    /// <summary>
    /// Получить ссылки на файлы.
    /// </summary>
    /// <param name="files">Файлы для получения ссылок.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Ссылки на файлы.</returns>
    Task<IReadOnlyCollection<FileWithUrlDto>> GetFilesUrl(IReadOnlyCollection<GetFileDto> files, CancellationToken cancellationToken);
    
    /// <summary>
    /// Удалить файл.
    /// </summary>
    /// <param name="bucketName">Имя хранилища.</param>
    /// <param name="fileName">Имя файла.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    Task RemoveFile(string bucketName, string fileName, CancellationToken cancellationToken);
}