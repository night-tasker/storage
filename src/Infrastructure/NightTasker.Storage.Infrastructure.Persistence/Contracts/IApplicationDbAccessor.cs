using NightTasker.Common.Core.Persistence;
using NightTasker.Storage.Domain.Entities;

namespace NightTasker.Storage.Infrastructure.Persistence.Contracts;

/// <summary>
/// Акссесор к базе данных.
/// </summary>
public interface IApplicationDbAccessor
{
    /// <summary>
    /// Загрузки.
    /// </summary>
    ApplicationDbSet<Upload, Guid> Uploads { get; }
    
    /// <summary>
    /// Сохранить изменения.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены.</param>
    Task SaveChanges(CancellationToken cancellationToken);
}