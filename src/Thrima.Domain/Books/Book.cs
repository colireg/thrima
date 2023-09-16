namespace Thrima.Domain.Books;

using System;
using Common;

public class Book : IEntity, IIdentifiable
{
    public required Guid Id { get; init; }
    public required string Title { get; set; }
    public string? Author { get; set; }
}