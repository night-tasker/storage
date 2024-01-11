using NightTasker.Common.Core.Abstractions;

namespace NightTasker.Storage.Domain.Entities;

/// <summary>
/// Загрузка.
/// </summary>
public class Upload : IEntityWithId<Guid>, ICreatedDateTimeOffset, IUpdatedDateTimeOffset
{
    /// <summary>
    /// Идентификатор.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Идентификатор пользователя.
    /// Если не задан, то загрузка не привязана к пользователю.
    /// </summary>
    public Guid? UserId { get; set; }

    /// <summary>
    /// Имя файла.
    /// </summary>
    public string? FileName { get; set; }

    /// <summary>
    /// Расширение.
    /// </summary>
    public string? Extension { get; set; }
    
    /// <inheritdoc />
    public DateTimeOffset CreatedDateTimeOffset { get; set; }

    /// <inheritdoc />
    public DateTimeOffset? UpdatedDateTimeOffset { get; set; }
}