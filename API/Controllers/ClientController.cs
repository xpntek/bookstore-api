using API.Serialization;
using Application.Dtos;
using Application.Security.Clients;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class ClientController:Controller

{
    private readonly IMediator _mediator;
    public ClientController(IMediator mediator)
    {
        _mediator = mediator;
      
    }

    [HttpPost]
    public async Task<IActionResult> CreateClient(CreateClientUser.CreateClientUserCommand command)
    {
        var result = await _mediator.Send(command);
        return this.SerializeResult(result);
    }

    [HttpGet]
    public async Task<IActionResult> ListClient()
    {
        var result = await _mediator.Send(new ListClientUser.ListClientUserQuery());
        return this.SerializeResult(result);
    }

    [HttpGet("id")]
    public async Task<IActionResult> ListClientById(int id)
    {
        var result = await _mediator.Send(new ListClientUserById.ListClientUserByIdIdQuery{ Id = id });
        return this.SerializeResult(result);

    }

    [HttpDelete("id")]
    public async Task<IActionResult> DeleteClient(int id)
    {
        var result = await _mediator.Send(new DeleteClientUser.DeleteClientUserCommand{Id=id});
        return this.SerializeResult(result);

    }

    [HttpPut("id")]
    public async Task<IActionResult> UpdateClient(int id,
        UpdateClientUser.UpdateClientUserCommand command)
    {
        command.Id = id;
        var result = await _mediator.Send(command);
        return this.SerializeResult(result);
    }
}