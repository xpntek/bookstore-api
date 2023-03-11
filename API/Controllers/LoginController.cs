using Application.Auths;
using Application.Dtos;
using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace API.Controllers;

public class LoginController:BaseApiController
{
     [HttpPost]
    [AllowAnonymous]
    public async  Task<ActionResult<Result<LoginResponse>>> Login(Login.LoginCommand command)
    {
        return await Mediator.Send(command);
    }
    
}