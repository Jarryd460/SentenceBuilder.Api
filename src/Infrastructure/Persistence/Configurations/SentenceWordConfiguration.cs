using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

internal class SentenceWordConfiguration : IEntityTypeConfiguration<SentenceWord>
{
    public void Configure(EntityTypeBuilder<SentenceWord> builder)
    {
        builder.HasKey(sentenceWord => new { sentenceWord.SentenceId, sentenceWord.WordId, sentenceWord.Order });

        builder.Ignore(sentenceWord => sentenceWord.Id);
    }
}
