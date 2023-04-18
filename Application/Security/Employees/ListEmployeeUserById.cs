using Application.Dtos;
using Application.Errors;
using Application.Interfaces;
using Application.Security.Specifications;
using AutoMapper;
using Domain;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Security;

public class ListEmployeeUserById
{
    public class ListEmployeeUserByIdQuery:IRequest<Result<EmployeeDto>>
    {
        public int Id { get; set; }
    }
    public class ListEmployeeUserByIdQueryHandler:IRequestHandler<ListEmployeeUserByIdQuery,Result<EmployeeDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ListEmployeeUserByIdQueryHandler(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;

        }
        public async Task<Result<EmployeeDto>> Handle(ListEmployeeUserByIdQuery request, CancellationToken cancellationToken)
        {
            var employee = new GetEmployeeByIdSpecification(request.Id);
            var check = await _unitOfWork.Repository<Employee>().GetEntityWithSpec(employee);
            if (check is null)
            {
                return Results.ConflictError(""+request.Id);
                
            }

            return _mapper.Map<EmployeeDto>(check);

        }
    }
}