using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

internal class WordConfiguration : IEntityTypeConfiguration<Word>
{
    public void Configure(EntityTypeBuilder<Word> builder)
    {
        builder.Property(word => word.Value)
            .HasMaxLength(100)
            .IsRequired();

        builder.HasIndex(word => word.Value)
            .IsUnique();

        builder.HasOne(word => word.WordType)
            .WithMany(wordType => wordType.Words)
            .HasForeignKey(word => word.WordTypeId);

        builder.HasMany(word => word.SentencesWords)
            .WithOne(sentenceWord => sentenceWord.Word)
            .IsRequired();
    }
}
