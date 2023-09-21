using Thrima.Domain.Common;

namespace Thrima.Domain.Books;

public class Book : IIdentifiable
{
    public required Guid Id { get; init; }
    public required string Title { get; set; }
    public required string Author { get; set; }
}