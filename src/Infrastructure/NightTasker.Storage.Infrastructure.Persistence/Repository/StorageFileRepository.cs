using NightTasker.Common.Core.Persistence;
using NightTasker.Common.Core.Persistence.Repository;
using NightTasker.Storage.Application.ApplicationContracts.Repository;
using NightTasker.Storage.Domain.Entities;

namespace NightTasker.Storage.Infrastructure.Persistence.Repository;

/// <inheritdoc cref="IStorageFileRepository" />
public class StorageFileRepository(ApplicationDbSet<StorageFile, Guid> dbSet) : BaseRepository<StorageFile, Guid>(dbSet),
    IStorageFileRepository;