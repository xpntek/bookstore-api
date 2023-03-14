using Application.Dtos;
using Application.Features.Authors;
using Domain;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Results = Application.Errors.Results;

namespace API.Controllers;

public class AuthorsController : BaseApiController
{
    private readonly IMediator _mediator;

    public AuthorsController( IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost]
    public async Task<Result<AuthorDto>> SaveAuthors( CreateAuthors.CreateAuthorsCommand command)
    {
        return await _mediator.Send(command);
    }
    
    [HttpGet]
    public async Task<Result<List<AuthorDto>>> ListAuthors()
    {
        return await _mediator.Send( new ListAllAuthors.ListAllAuthorsQuery());
    }
    
    [HttpGet("{id}")]
    public async Task<Result<AuthorDto>> GetAuthorsById( int id)
    {
        return await _mediator.Send(new ListAuthorById.ListAuthorByIdQuery{Id = id});
    }

    [HttpGet("fullname/{fullname}")]
    public async Task<Result<AuthorDto>> GetAuthorsByFullName( string fullname)
    {
        return await _mediator.Send(new GetAuthorByFullName.GetAuthorByFullNameQuery{FullName = fullname});
    }
    
    

    [HttpDelete("{id}")]
    public async Task<Result<AuthorDto>> DeleteAuthor(int id)
    {
        return await _mediator.Send(new DeleteAuthor.DeleteAuthorCommand { Id = id });
    }
    
    [HttpPut("{id}")]
    public async Task<Result<AuthorDto>> UpdateAuthors(int id, UpdateAuthor.UpdateAuthorCommand command)
    {
        command.Id = id;
        return await _mediator.Send(command);
    }

    


    
}