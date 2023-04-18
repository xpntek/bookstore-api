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

public class DeleteEmployeeUser
{
    public class DeleteEmployeeUserCommand:IRequest<Result<EmployeeDto>>
    {
        public int Id { get; set; }
        
    }
    public class DeleteEmployeeUserCommandHandler:IRequestHandler<DeleteEmployeeUserCommand,Result<EmployeeDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        
        public DeleteEmployeeUserCommandHandler(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;

        }
        public async Task<Result<EmployeeDto>> Handle(DeleteEmployeeUserCommand request, CancellationToken cancellationToken)
        {
           
            var employee = await _unitOfWork.Repository<Employee>().GetByIdAsync(request.Id);
            if (employee is null)
            {
                 return Results.NotFoundError("Id: " + request.Id);
            }
            _unitOfWork.Repository<Employee>().Delete(employee);
            var resul = await _unitOfWork.Complete();
            if (resul < 0)
            {
                return Results.InternalError("Departament don't  deleted");
            }

            return _mapper.Map<EmployeeDto>(employee);
        
        }
    }
    
}