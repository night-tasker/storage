using NightTasker.Common.Core.Abstractions;

namespace NightTasker.Storage.Domain.Entities;

/// <summary>
/// Файл.
/// </summary>
public class StorageFile : IEntityWithId<Guid>, ICreatedDateTimeOffset
{
    /// <summary>
    /// Идентификатор.
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Идентификатор пользователя, создавшего файл.
    /// </summary>
    public Guid UploadedByUserId { get; set; }

    /// <summary>
    /// Название файла.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Расширение файла.
    /// </summary>
    public string? Extension { get; set; }

    /// <summary>
    /// Дата загрузки файла.
    /// </summary>
    public DateTimeOffset CreatedDateTimeOffset { get; set; }
}