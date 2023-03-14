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
    public async Task<Result<BookDto>> SaveBookAuthor(CreateBookAuthor.CreateBookCommand command)
    {
        return await _mediator.Send(command);
    }

    [HttpGet]
    public async Task<Result<List<BookDto>>> ListBookAuthor()
    {
        return await _mediator.Send(new ListBooksAuthors.ListBooksAuthorsQuery());
    }

    [HttpGet("{id}")]
    public async Task<Result<BookDto>> ListBookAuthor(int id)
    {
        return await _mediator.Send(new ListBookById.ListBookByIdQuery { Id = id });
    }

    [HttpPut("{id}")]
    public async Task<Result<BookDto>> UpdateBookAuthor(int id, UpdateBookAuthor.UpdateBookAuthorCommand command)
    {
        command.Id = id;
        return await _mediator.Send(command);
    }


    [HttpDelete("{id}")]
    public async Task<Result<BookDto>> DeleteBookAuthor(int id)
    {
        return await _mediator.Send(new DeleteBookAuthors.DeleteBookAuthorsCommand { Id = id });
    }
}