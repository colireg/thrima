using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Thrima.Application;
using Thrima.Application.Services;
using Thrima.Domain.Books;

namespace Thrima.Infrastructure.Persistence;

public class AppDbContext : DbContext, IAppDbContext
{
    public DbSet<Book> Books { get; init; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
}
