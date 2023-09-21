using MediatR;
using Microsoft.AspNetCore.Mvc;
using Thrima.Application;
using Thrima.Application.Books.Commands;
using Thrima.Application.Books.Queries;
using Thrima.WebApi.Common;

namespace Thrima.WebApi.Controllers;

public class BookController : ApiController
{
    private readonly ISender _mediator;
    public BookController(ISender mediator) 
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAsync(GetAllBooksQuery query, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(query, cancellationToken);
        return Ok(result);
    }

    // [HttpGet("{id:Guid}")]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetAsync([FromQuery] GetBookByIdQuery query, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(query, cancellationToken);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync(CreateBookCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync([FromQuery] DeleteBookCommand command, CancellationToken cancellationToken)
    {
        await _mediator.Send(command, cancellationToken);
        return Ok();
    }

}
