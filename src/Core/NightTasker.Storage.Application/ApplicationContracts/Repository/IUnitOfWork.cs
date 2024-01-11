namespace NightTasker.Storage.Application.ApplicationContracts.Repository;

/// <summary>
/// Unit of work.
/// </summary>
public interface IUnitOfWork
{
    /// <inheritdoc cref="IUploadRepository"/>
    IUploadRepository UploadRepository { get; }
    
    /// <summary>
    /// Сохранить изменения.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены.</param>
    Task SaveChanges(CancellationToken cancellationToken);
}