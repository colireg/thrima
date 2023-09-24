using MediatR;
using Thrima.Domain.Books;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Thrima.Application.Services;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using System.Data;

namespace Thrima.Application.Books.Queries;

public record GetAllBooksQuery(
    string? Title,
    string? Author) : IRequest<IEnumerable<GetAllBooksQueryListItem>>;

public class GetAllBooksQueryValidator : AbstractValidator<GetAllBooksQuery>
{
    public GetAllBooksQueryValidator()
    {
        RuleFor(x => x.Title)
            .MaximumLength(256);
        
        RuleFor(x => x.Author)
            .MaximumLength(256);
    }
}

public record GetAllBooksQueryListItem(
    Guid Id,
    string Title,
    string Author);

public class BookToGetAllBooksQueryListItemMapper : Profile
{
    public BookToGetAllBooksQueryListItemMapper()
    {
        CreateMap<Book, GetAllBooksQueryListItem>();
    }
}

public class GetAllBooksQueryHandler
    : IRequestHandler<GetAllBooksQuery, IEnumerable<GetAllBooksQueryListItem>>
{
    private readonly IMapper _mapper;
    private readonly IAppDbContext _appDbContext;
    private readonly IValidator<GetAllBooksQuery> _validator;

    public GetAllBooksQueryHandler(
        IMapper mapper,
        IAppDbContext appDbContext,
        IValidator<GetAllBooksQuery> validator)
    {
        _mapper = mapper;
        _appDbContext = appDbContext;
        _validator = validator;
    }

    public async Task<IEnumerable<GetAllBooksQueryListItem>> Handle(GetAllBooksQuery request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);

        var query = _appDbContext
            .Books
            .AsNoTracking()
            .AsQueryable();
        
        if (request.Title is not null)
            query = query.Where(x => x.Title.StartsWith(request.Title));

        if (request.Author is not null)
            query = query.Where(x => x.Author.StartsWith(request.Author));

        var result = await query
            .ProjectTo<GetAllBooksQueryListItem>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return result;
    }
}

