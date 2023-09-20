using Microsoft.EntityFrameworkCore;
using Thrima.Domain.Books;

namespace Thrima.Application.Services;

public interface IAppDbContext
{
    DbSet<Book> Books { get; }

    int SaveChanges();
    int SaveChanges(bool acceptAllChangesOnSuccess);
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default);
}
