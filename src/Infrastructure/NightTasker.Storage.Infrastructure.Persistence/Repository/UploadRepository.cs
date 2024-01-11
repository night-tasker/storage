using NightTasker.Common.Core.Persistence;
using NightTasker.Common.Core.Persistence.Repository;
using NightTasker.Storage.Application.ApplicationContracts.Repository;
using NightTasker.Storage.Domain.Entities;

namespace NightTasker.Storage.Infrastructure.Persistence.Repository;

/// <inheritdoc cref="NightTasker.Storage.Application.ApplicationContracts.Repository.IUploadRepository" />
public class UploadRepository(ApplicationDbSet<Upload, Guid> dbSet) : BaseRepository<Upload, Guid>(dbSet),
    IUploadRepository;