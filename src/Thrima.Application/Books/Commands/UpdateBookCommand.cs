using System.Data;
using AutoMapper;
using FluentValidation;
using MediatR;
using Thrima.Application.Exceptions;
using Thrima.Application.Services;
using Thrima.Domain.Books;

namespace Thrima.Application;

public record UpdateBookCommand(
    Guid Id,
    string Title,
    string Author) : IRequest<UpdateBookResponse>;

public class UpdateBookCommandValidator : AbstractValidator<UpdateBookCommand>
{
    public UpdateBookCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();

        RuleFor(x => x.Title).NotEmpty().MaximumLength(255);

        RuleFor(x => x.Author).NotEmpty().MaximumLength(255);
    }
}

public record UpdateBookResponse(
    Guid Id,
    string Title,
    string Author);

public class BookToUpdateBookResponseMapper : Profile
{
    public BookToUpdateBookResponseMapper()
    {
        CreateMap<Book, UpdateBookResponse>();
    }
}

public class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand, UpdateBookResponse>
{
    private readonly IAppDbContext _appDbContext;
    private readonly IValidator<UpdateBookCommand> _validator;
    private readonly IMapper _mapper;

    public UpdateBookCommandHandler(
        IAppDbContext appDbContext,
        IValidator<UpdateBookCommand> validator,
        IMapper mapper)
    {
            _appDbContext = appDbContext;
            _validator = validator;
            _mapper = mapper;
    }

    public async Task<UpdateBookResponse> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
    {
        var entity = await _appDbContext.Books.FindAsync(request.Id, cancellationToken)
            ?? throw new EntityNotFoundException();
        
        entity.Title = request.Title;
        entity.Author = request.Author;

        _appDbContext.Books.Update(entity);

        await _appDbContext.SaveChangesAsync(cancellationToken);

        return _mapper.Map<UpdateBookResponse>(entity);
    }
}



