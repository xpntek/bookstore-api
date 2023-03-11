using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Application.Dtos;
using Application.Errors;
using AutoMapper;
using Domain;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Identity;
namespace Application.Security;
    public class CreateUser
    {
        public class CreateUserCommand:IRequest<Result<UserDto>>{
             public string Email { get; set; }
             public string FullName { get; set; }
             public string UserName { get; set; }
             public string PhoneNumber { get; set; }
             public string Address { get; set; }
             public string Password { get; set; }
        }

        public class CreateUserCommandHandler: IRequestHandler<CreateUserCommand,Result<UserDto>>{
             private readonly IMapper _mapper;
             private readonly UserManager<ApplicationUser> _userManager;

            public CreateUserCommandHandler(IMapper mapper,UserManager<ApplicationUser> userManager){
                 _mapper = mapper;
                _userManager = userManager;
            }

            public async Task<Result<UserDto>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
            {
               var check = await _userManager.FindByEmailAsync(request.Email);
              if (check is not null)
                {
                     return Results.ConflictError("Email Exists");
                }
                
                
                var user = new ApplicationUser()
                {
                    Email = request.Email,
                    Fullname = request.FullName,
                    UserName = request.UserName,
                    PhoneNumber = request.PhoneNumber,
                    Address = request.Address
                };
              
            
              var result = await _userManager.CreateAsync(user, request.Password);
              
               if (result.Succeeded)
               {
                   return  _mapper.Map<UserDto>(user);
                   
               }

               return Results.InternalError("Error While saving");
               



            }


          
        }
    }
