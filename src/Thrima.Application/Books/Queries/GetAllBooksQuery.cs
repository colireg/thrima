using MediatR;
using Thrima.Domain.Books;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Thrima.Application.Services;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace Thrima.Application.Books.Queries;

public record GetAllBooksQuery() : IRequest<IEnumerable<GetAllBooksQueryListItem>>;

public record GetAllBooksQueryListItem(
    Guid Id,
    string Title,
    string Author);

public class BookToGetAllBooksQueryListItemMapper : Profile
{
    public BookToGetAllBooksQueryListItemMapper()
    {
        CreateMap<Book, GetAllBooksQuery>();
    }
}

public class GetAllBooksQueryHandler
    : IRequestHandler<GetAllBooksQuery, IEnumerable<GetAllBooksQueryListItem>>
{
    private readonly IMapper _mapper;
    private readonly IAppDbContext _appDbContext;

    public GetAllBooksQueryHandler(IMapper mapper, IAppDbContext appDbContext)
    {
        _mapper = mapper;
        _appDbContext = appDbContext;
    }

    public async Task<IEnumerable<GetAllBooksQueryListItem>> Handle(GetAllBooksQuery request, CancellationToken cancellationToken)
    {
        var result = await _appDbContext
            .Books
            .AsNoTracking()
            .ProjectTo<GetAllBooksQueryListItem>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return result;
    }
}

