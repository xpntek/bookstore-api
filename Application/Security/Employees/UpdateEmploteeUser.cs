using Application.Dtos;
using Application.Errors;
using Application.Interfaces;
using AutoMapper;
using Domain;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Security;

public class UpdateEmployeeUser
{
    public class UpdateEmployeeUserCommand:IRequest<Result<EmployeeDto>>{
             public int Id {get;set;}
             public string Email { get; set; }
             public string FullName { get; set; }
             public string UserName { get; set; }
             public string PhoneNumber { get; set; }
             public string Address { get; set; }
            
             public int EmployeeCode { get; set; }
             public string Function { get; set; }
             public float Salary { get; set; }
             public DateTime HiringDate  { get; set; }
             public string Status  { get; set; }
             public int DepartamentId  { get; set; }
    }
    public class UpdateEmployeeUserCommandHandler:IRequestHandler<UpdateEmployeeUserCommand,Result<EmployeeDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateEmployeeUserCommandHandler(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;

        }
        public async Task<Result<EmployeeDto>> Handle(UpdateEmployeeUserCommand request, CancellationToken cancellationToken)
        {
            var check = await _unitOfWork.Repository<Employee>().GetByIdAsync(request.Id);
            if (check is null)
            {
                return Results.NotFoundError("Employee not found");
            }

            check.ApplicationUser.Email = request.Email;
            check.ApplicationUser.Address = request.Address;
            check.ApplicationUser.Fullname = request.FullName;
            check.ApplicationUser.PhoneNumber = request.PhoneNumber;
            check.ApplicationUser.UserName = request.UserName;
            check.EmployeeCode = request.EmployeeCode;
            check.Function = request.Function;
            check.Salary = request.Salary;
            check.HiringDate = new DateTimeOffset(request.HiringDate);
            check.Status = request.Status;
            check.DepartamentId = request.DepartamentId;
            _unitOfWork.Repository<Employee>().Update(check);
            var resul = await _unitOfWork.Complete();
            if (resul > 0)
            {
                return _mapper.Map<EmployeeDto>(check);

            }
            return Results.InternalError("Error While saving");

        }
    }
    
}