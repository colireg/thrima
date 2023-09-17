using Microsoft.EntityFrameworkCore;
using Thrima.Domain.Books;

namespace Thrima.Application.Services;

public interface IAppDbContext
{
    DbSet<Book> Books { get; }
}
