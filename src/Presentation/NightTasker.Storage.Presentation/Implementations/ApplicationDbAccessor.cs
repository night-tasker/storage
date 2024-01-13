using NightTasker.Common.Core.Identity.Contracts;
using NightTasker.Common.Core.Persistence;
using NightTasker.Storage.Domain.Entities;
using NightTasker.Storage.Infrastructure.Persistence;
using NightTasker.Storage.Infrastructure.Persistence.Contracts;

namespace NightTasker.Storage.Presentation.Implementations;

/// <inheritdoc /> 
public class ApplicationDbAccessor : IApplicationDbAccessor
{
    private readonly ApplicationDbContext _dbContext;

    public ApplicationDbAccessor(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        Files = new ApplicationDbSet<StorageFile, Guid>(_dbContext);
    }
    
    /// <inheritdoc />
    public ApplicationDbSet<StorageFile, Guid> Files { get; }
    
    private IQueryable<T> EmptyQuery<T>(IQueryable<T> query) => Enumerable.Empty<T>().AsQueryable();

    /// <inheritdoc />
    public Task SaveChanges(CancellationToken cancellationToken)
    {
        return _dbContext.SaveChangesAsync(cancellationToken);
    }
}