using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Serialization;
using Application.Dtos;
using Application.Security;
using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
    public class EmployeeUserController:BaseApiController
    {

        [HttpPost]
        public async Task<IActionResult> CreateEmployeeUser( CreateEmployeeUser.CreateEmployeeUserCommand  command)
        {
            var result = await Mediator.Send(command);
            return this.SerializeResult(result);
        }

        [HttpDelete("id")]
        public async Task<IActionResult> DeleteEmployeeUser(int id)
        {
            var result =  await Mediator.Send(new DeleteEmployeeUser.DeleteEmployeeUserCommand {Id= id});
            return this.SerializeResult(result);
        }
         [HttpPut("id")]
         public async Task<IActionResult> UpdateUser(int id,UpdateEmployeeUser.UpdateEmployeeUserCommand command) 
         {
             command.Id= id;
             var result = await Mediator.Send(command);
             return this.SerializeResult(result);
         }

        [HttpGet]
        public async Task<IActionResult> ListEmployeeUser()
        {
            var result = await Mediator.Send(new ListEmployeeUser.ListEmployeeUserQuery());
            return this.SerializeResult(result);
        }
        [HttpGet("id")]
        public async Task<IActionResult> ListEmployeeUserById(int id)
        {
            var result = await Mediator.Send(new ListEmployeeUserById.ListEmployeeUserByIdQuery{Id = id});
            return this.SerializeResult(result);
        }

    }
