﻿using NightTasker.Storage.Application.ApplicationContracts.Repository;
using NightTasker.Storage.Infrastructure.Persistence.Contracts;

namespace NightTasker.Storage.Infrastructure.Persistence.Repository.Common;

/// <inheritdoc />
public class UnitOfWork(IApplicationDbAccessor applicationDbAccessor) : IUnitOfWork
{
    /// <inheritdoc />
    public IStorageFileRepository StorageFileRepository { get; } = new StorageFileRepository(applicationDbAccessor.Files);

    /// <inheritdoc /> 
    public Task SaveChanges(CancellationToken cancellationToken)
    {
        return applicationDbAccessor.SaveChanges(cancellationToken);
    }
}