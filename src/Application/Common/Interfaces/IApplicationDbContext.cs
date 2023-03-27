using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<WordType> WordTypes { get; }
    DbSet<Word> Words { get; }
    DbSet<Sentence> Sentences { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
