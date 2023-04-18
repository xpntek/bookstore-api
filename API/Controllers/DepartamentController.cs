using API.Serialization;
using Application.Dtos;
using Application.Features.Departaments;
using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class DepartamentController:BaseApiController
{
    [HttpPost]
    public async Task<IActionResult> CreateDepartament(
        CreateDepartament.CreateDepartamentCommand command)
    {
        var result = await Mediator.Send(command);
        return this.SerializeResult(result);
    }

    [HttpGet]
    public async Task<IActionResult> ListDepartament()
    {
        var result= await Mediator.Send(new ListDepartament.LisDepartamentQuery());
        return this.SerializeResult(result);
    }

    [HttpGet("id")]
    public async Task<IActionResult> ListDepartamentById(int id)
    {
        var result = await Mediator.Send(new ListDepartamentByID.ListDepartamentByIDQuery { Id = id });
        return this.SerializeResult(result);
    }

    [HttpDelete("id")]
    public async Task<IActionResult> DeleteDepartament(int id)
    {
        var result = await Mediator.Send(new DeleteDepartament.DeleteDeartamentCommand{Id=id});
        return this.SerializeResult(result);

    }
    [HttpPut("id")]
    public async Task<IActionResult> UpdateDepartament(int id,UpdateDepartament.UpdateDepartamentCommand command)
    {
        command.Id = id;
        var result = await Mediator.Send(command);
        return this.SerializeResult(result);

    }
    [HttpGet("description")]
    public async Task<IActionResult> GetDepartamentByName( string description)
    {
        var result = await Mediator.Send(new GetDepartamentByName.GetDepartamentByNameQuery{Description = description});
        return this.SerializeResult(result);
    }
}