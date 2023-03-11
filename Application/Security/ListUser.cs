using Application.Dtos;
using Application.Interfaces;
using AutoMapper;
using Domain;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Security;

public class ListUser
{
    public class ListUserQuery:IRequest<List<UserDto>>
    {
        
    }
    public class ListUserQueryHandler:IRequestHandler<ListUserQuery,List<UserDto>>
    {
        private readonly UserManager<ApplicationUser> _manager;
        private readonly IMapper _mapper;
       
        public ListUserQueryHandler(UserManager<ApplicationUser> manager,IMapper mapper)
        {
            _manager = manager;
            _mapper = mapper;
           

        }
        public async Task<List<UserDto>> Handle(ListUserQuery request, CancellationToken cancellationToken)
        {
            var list = await _manager.Users.ToListAsync();
          
            return _mapper.Map<List<UserDto>>(list);

        }
    }
}