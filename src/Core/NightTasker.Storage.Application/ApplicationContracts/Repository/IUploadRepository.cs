using NightTasker.Common.Core.Persistence.Repository;
using NightTasker.Storage.Domain.Entities;

namespace NightTasker.Storage.Application.ApplicationContracts.Repository;

/// <summary>
/// Репозиторий для <see cref="Upload"/>.
/// </summary>
public interface IUploadRepository : IRepository<Upload, Guid>
{
    
}