using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

internal class WordTypeConfiguration : IEntityTypeConfiguration<WordType>
{
    public void Configure(EntityTypeBuilder<WordType> builder)
    {
        builder.Property(wordType => wordType.Value)
            .IsRequired();

        builder.HasIndex(wordType => wordType.Value)
            .IsUnique();
    }
}
