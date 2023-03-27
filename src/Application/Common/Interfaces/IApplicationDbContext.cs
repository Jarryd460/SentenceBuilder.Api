using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<WordType> WordTypes { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
