using API.Serialization;
using Application.Dtos;
using Application.Features.Books;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class BooksController : BaseApiController
{
    private readonly IMediator _mediator;

    public BooksController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> SaveBookAuthor(CreateBookAuthor.CreateBookCommand command)
    {
        var result = await _mediator.Send(command);
        return this.SerializeResult(result);
    }

    [HttpGet]
    public async Task<IActionResult> ListBookAuthor()
    {
        var result = await _mediator.Send(new ListBooksAuthors.ListBooksAuthorsQuery());
        return this.SerializeResult(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> ListBookAuthor(int id)
    {
        var result = await _mediator.Send(new ListBookById.ListBookByIdQuery { Id = id });
        return this.SerializeResult(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateBookAuthor(int id, UpdateBookAuthor.UpdateBookAuthorCommand command)
    {
        command.Id = id;
        var result = await _mediator.Send(command);
        return this.SerializeResult(result);
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBookAuthor(int id)
    {
        var result = await _mediator.Send(new DeleteBookAuthors.DeleteBookAuthorsCommand { Id = id });
        return this.SerializeResult(result);
    }
}