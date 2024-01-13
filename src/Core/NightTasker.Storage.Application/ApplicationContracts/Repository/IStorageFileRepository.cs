using NightTasker.Common.Core.Persistence.Repository;
using NightTasker.Storage.Domain.Entities;

namespace NightTasker.Storage.Application.ApplicationContracts.Repository;

/// <summary>
/// Репозиторий для <see cref="StorageFile"/>.
/// </summary>
public interface IStorageFileRepository : IRepository<StorageFile, Guid>
{
    
}