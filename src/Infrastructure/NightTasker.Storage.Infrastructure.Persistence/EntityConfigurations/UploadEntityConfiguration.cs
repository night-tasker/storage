using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NightTasker.Storage.Domain.Entities;

namespace NightTasker.Storage.Infrastructure.Persistence.EntityConfigurations;

/// <summary>
/// Конфигурация сущности <see cref="Upload"/>
/// </summary>
public class UploadEntityConfiguration : IEntityTypeConfiguration<Upload>
{
    public void Configure(EntityTypeBuilder<Upload> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.UserId)
            .IsRequired(false);

        builder.Property(x => x.FileName)
            .HasMaxLength(254);

        builder.Property(x => x.Extension)
            .HasMaxLength(32);

        builder.Property(x => x.CreatedDateTimeOffset);
        
        builder.Property(x => x.UpdatedDateTimeOffset);
    }
}