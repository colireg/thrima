using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Thrima.Application.Exceptions;
using Thrima.Application.Services;
using Thrima.Domain.Books;

namespace Thrima.Application.Books.Queries;

public record GetBookByIdQuery(Guid Id) : IRequest<GetBookByIdQueryResponse>;

public record GetBookByIdQueryResponse(
    string Title,
    string Author);

public class BookToGetBookByIdQueryResponseMapper : Profile
{
    public BookToGetBookByIdQueryResponseMapper()
    {
        CreateMap<Book, GetBookByIdQueryResponse>();
    }
}

public class GetBookByIdQueryHandler
    : IRequestHandler<GetBookByIdQuery, GetBookByIdQueryResponse>
{
    private readonly IAppDbContext _appDbContext;
    private readonly IMapper _mapper;

    public GetBookByIdQueryHandler(IAppDbContext appDbContext, IMapper mapper)
    {
        _appDbContext = appDbContext;
        _mapper = mapper;
    }

    public async Task<GetBookByIdQueryResponse> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _appDbContext
            .Books
            .AsNoTracking()
            .Where(x => x.Id == request.Id)
            .ProjectTo<GetBookByIdQueryResponse>(_mapper.ConfigurationProvider)
            .SingleOrDefaultAsync(cancellationToken)
            ?? throw new EntityNotFoundException();
            
        return result;
    }
}