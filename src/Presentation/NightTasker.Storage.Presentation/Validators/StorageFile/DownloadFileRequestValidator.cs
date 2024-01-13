using FluentValidation;
using NightTasker.Storage.Presentation.Requests.StorageFile;

namespace NightTasker.Storage.Presentation.Validators.StorageFile;

/// <summary>
/// Валидатор для <see cref="DownloadFileRequest"/>.
/// </summary>
public class DownloadFileRequestValidator : AbstractValidator<DownloadFileRequest>
{
    public DownloadFileRequestValidator()
    {
        RuleFor(x => x.BucketName)
            .NotEmpty();

        RuleFor(x => x.FileName)
            .NotEmpty();
    }
}