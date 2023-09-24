using AutoMapper;
using FluentValidation;
using MediatR;
using Thrima.Application.Services;
using Thrima.Domain.Books;

namespace Thrima.Application.Books.Commands;

public record CreateBookCommand(
    string Title,
    string Author) : IRequest<CreateBookCommandResponse>;

public class CreateBookCommandToBookMapper : Profile
{
    public CreateBookCommandToBookMapper()
    {
        CreateMap<CreateBookCommand, Book>();
    }
}

public class CreateBookCommandValidator : AbstractValidator<CreateBookCommand>
{
    public CreateBookCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .MaximumLength(256);
        
        RuleFor(x => x.Author)
            .NotEmpty()
            .MaximumLength(256);
    }
}

public record CreateBookCommandResponse(Guid Id);

public class BookToCreateBookCommandResponseMapper : Profile
{
    public BookToCreateBookCommandResponseMapper()
    {
        CreateMap<Book, CreateBookCommandResponse>();
    }
}

public class CreateBookCommandHandler :
    IRequestHandler<CreateBookCommand, CreateBookCommandResponse>
{
    private readonly IAppDbContext _appDbContext;
    private readonly IValidator<CreateBookCommand> _validator;
    private readonly IMapper _mapper;
    public CreateBookCommandHandler(
            IAppDbContext appDbContext,
            IValidator<CreateBookCommand> validator,
            IMapper mapper)
    {
        _appDbContext = appDbContext;
        _validator = validator;
        _mapper = mapper;
    }

    public async Task<CreateBookCommandResponse> Handle(CreateBookCommand request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);
        var entity = _mapper.Map<Book>(request);
        await _appDbContext.Books.AddAsync(entity, cancellationToken);
        await _appDbContext.SaveChangesAsync(cancellationToken);
        return _mapper.Map<CreateBookCommandResponse>(entity);
    }
}