using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Thrima.Application.Exceptions;
using Thrima.Application.Services;

namespace Thrima.Application;

public record DeleteBookCommand(Guid Id) : IRequest;

public class DeleteBookCommandValidator : AbstractValidator<DeleteBookCommand>
{
    public DeleteBookCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}

public class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommand>
{
    private readonly IAppDbContext _appDbContext;
    private readonly IValidator<DeleteBookCommand> _validator;
    public DeleteBookCommandHandler(
        IAppDbContext appDbContext,
        IValidator<DeleteBookCommand> validator)
    {
        _appDbContext = appDbContext;
        _validator = validator;
    }

    public async Task Handle(DeleteBookCommand request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);
        var entity = await _appDbContext
            .Books
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken)
            ?? throw new EntityNotFoundException();
        _appDbContext.Books.Remove(entity);
        await _appDbContext.SaveChangesAsync(cancellationToken);
    }
}