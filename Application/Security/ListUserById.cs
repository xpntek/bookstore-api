using Application.Dtos;
using Application.Errors;
using AutoMapper;
using Domain;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Security;

public class ListUserById
{
    public class LisUserByIdQuery:IRequest<Result<UserDto>>
    {
        public string ID { get; set; }
    }
    public class LisUserByIdQueryHandler:IRequestHandler<LisUserByIdQuery,Result<UserDto>>
    {
        private readonly UserManager<ApplicationUser> _manager;
        private readonly IMapper _mapper;
        public LisUserByIdQueryHandler(UserManager<ApplicationUser> manager,IMapper mapper)
        {
            _manager = manager;
            _mapper = mapper;

        }
        public async Task<Result<UserDto>> Handle(LisUserByIdQuery request, CancellationToken cancellationToken)
        {
            var check = await _manager.FindByIdAsync(request.ID);
            if (check is null)
            {
                return Results.ConflictError(request.ID);
                
            }

            return _mapper.Map<UserDto>(check);

        }
    }
}