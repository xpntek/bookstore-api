using Application.Dtos;
using Application.Features.Favorites;
using Domain;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class FavoriteController : BaseApiController
{
    private readonly IMediator _mediator;

    public FavoriteController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<Result<FavoriteDto>> SaveFavorite(CreateFavorite.CreateFavoriteCommand command)
    {
        return await _mediator.Send(command);
    }
    
    [HttpGet]
    public async Task<Result<List<BookDto>>> ListFavorite()
    {
        return await _mediator.Send(new ListFavorite.ListFavoriteQuery());
    }
    
    [HttpDelete("{id}")]
    public async Task<Result<Favorite>> DeleteFavorite(int id)
    {
        return await _mediator.Send(new DeleteFavorite.DeleteFavoriteCommand{Id = id});
    }
}