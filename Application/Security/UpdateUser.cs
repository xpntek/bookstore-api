using Application.Dtos;
using Application.Errors;
using AutoMapper;
using Domain;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Security;

public class UpdateUser
{
    public class UpdateUserCommand:IRequest<Result<UserDto>>{
             public string ID {get;set;}
             public string Email { get; set; }
             public string FullName { get; set; }
             public string UserName { get; set; }
             public string PhoneNumber { get; set; }
             public string Address { get; set; }
             public string Password { get; set; }
    }
    public class UpdateUserCommandHandler:IRequestHandler<UpdateUserCommand,Result<UserDto>>
    {
        private readonly UserManager<ApplicationUser> _user;
        private readonly IMapper _mapper;

        public UpdateUserCommandHandler(UserManager<ApplicationUser> user,IMapper mapper)
        {
            _user = user;
            _mapper = mapper;

        }
        public async Task<Result<UserDto>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var check = await _user.FindByIdAsync(request.ID);
            if (check is null)
            {
                return Results.NotFoundError("User not found");
            }

            check.Email = request.Email;
            check.Address = request.Address;
            check.Fullname = request.FullName;
            check.PhoneNumber = request.PhoneNumber;
            check.UserName = request.UserName;
            var resul = await _user.UpdateAsync(check);
            if (resul.Succeeded)
            {
                return _mapper.Map<UserDto>(check);

            }
            return Results.InternalError("Error While saving");

        }
    }
    
}