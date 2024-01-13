using NightTasker.Common.Core.Exceptions.Base;

namespace NightTasker.Storage.Application.Exceptions.NotFound;

/// <summary>
/// Файл в хранилище не найден.
/// </summary>
public class StorageFileNotFoundException(string message) : NotFoundException(message);