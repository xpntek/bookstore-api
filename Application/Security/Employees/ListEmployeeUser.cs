using Application.Dtos;
using Application.Interfaces;
using Application.Security.Specifications;
using AutoMapper;
using Domain;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Security;

public class ListEmployeeUser
{
    public class ListEmployeeUserQuery:IRequest<Result<List<EmployeeDto>>>
    {
        
    }
    public class ListEmployeeUserQueryHandler:IRequestHandler<ListEmployeeUserQuery,Result<List<EmployeeDto>>>
    {
        private readonly IUnitOfWork _iUnitOfWork;
        private readonly IMapper _mapper;
       
        public ListEmployeeUserQueryHandler(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _iUnitOfWork = unitOfWork;
            _mapper = mapper;
           

        }
        public async Task<Result<List<EmployeeDto>>> Handle(ListEmployeeUserQuery request, CancellationToken cancellationToken)
        {
            var employee = new AllEmployeeSpecification();
            var list = await _iUnitOfWork.Repository<Employee>().ListWithSpecAsync(employee);
            return _mapper.Map<List<EmployeeDto>>(list);

        }
    }
}