using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Dtos;
using Application.Security;
using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
    public class UserController:BaseApiController
    {

        [HttpPost]
        public async Task<ActionResult<Result<UserDto>>> CreateUser(CreateUser.CreateUserCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpDelete("id")]
        public async Task<ActionResult<Result<UserDto>>> DeleteUser(String id)
        {
            return await Mediator.Send(new DeleteUser.DeleteUserCommand {ID = id});
        }
         [HttpPut("id")]
         public async Task<ActionResult<Result<UserDto>>> UpdateUser(String id,UpdateUser.UpdateUserCommand command) 
         {
             command.ID= id;
             return await Mediator.Send(command);
         }

        [HttpGet]
        public async Task<ActionResult<List<UserDto>>> ListUser()
        {
            return await Mediator.Send(new ListUser.ListUserQuery());
        }
        [HttpGet("id")]
        public async Task<ActionResult<Result<UserDto>>> LisUserById(String id)
        {
            return await Mediator.Send(new ListUserById.LisUserByIdQuery {ID = id});
        }

    }
