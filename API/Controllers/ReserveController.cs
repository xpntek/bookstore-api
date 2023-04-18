using API.Serialization;
using Application.Features.Reserves;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class ReserveController:BaseApiController
{
    [HttpPost]
    public async Task<IActionResult> CreateReserve(
        CreateReserve.CreateReserveCommand command)
    {
        var result = await Mediator.Send(command);
        return this.SerializeResult(result);
    }

    [HttpGet]
    public async Task<IActionResult> ListReserve()
    {
        var result= await Mediator.Send(new ListReserve.ListReserveQuery());
        return this.SerializeResult(result);
    }

    [HttpGet("id")]
    public async Task<IActionResult> ListReserveById(int id)
    {
        var result = await Mediator.Send(new GetReserveById.GetReserveByIdQuery { Id = id });
        return this.SerializeResult(result);
    }

    [HttpDelete("id")]
    public async Task<IActionResult> DeleteReserve(int id)
    {
        var result = await Mediator.Send(new DeleteReserve.DeleteReserveCommand{Id=id});
        return this.SerializeResult(result);

    }
    [HttpPut("id")]
    public async Task<IActionResult> UpdateDepartament(int id,UpdateReserve.UpdateReserveCommand command)
    {
        command.Id = id;
        var result = await Mediator.Send(command);
        return this.SerializeResult(result);

    }
   
    
}