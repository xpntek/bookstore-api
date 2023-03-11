using Application.Dtos;
using Application.Errors;
using AutoMapper;
using Domain;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Security;

public class DeleteUser
{
    public class DeleteUserCommand:IRequest<Result<UserDto>>
    {
        public string ID { get; set; }
        
    }
    public class DeleteUserCommandHandler:IRequestHandler<DeleteUserCommand,Result<UserDto>>
    {
        private readonly UserManager<ApplicationUser> _manager;
        private readonly IMapper _mapper;
        
        public DeleteUserCommandHandler(UserManager<ApplicationUser> manager,IMapper mapper)
        {
            _manager = manager;
            _mapper = mapper;

        }
        public async Task<Result<UserDto>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var check = await _manager.FindByIdAsync(request.ID);
            if (check is null)
            {
                Results.NotFoundError(request.ID);

            }

            var result = await _manager.DeleteAsync(check);
            if (!result.Succeeded)
            {
                return Results.InternalError("Error while delete user");
            }

            return _mapper.Map<ApplicationUser,UserDto>(check);

        }
    }
    
}