using Application.Dtos;
using Application.Errors;
using Application.Interfaces;
using AutoMapper;
using Domain;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Persistence;

    namespace Application.Security;
    public class  CreateEmployeeUser
    {
        public class CreateEmployeeUserCommand:IRequest<Result<EmployeeDto>>{
             public string Email { get; set; }
             public string FullName { get; set; }
             public string UserName { get; set; }
             public string PhoneNumber { get; set; }
             public string Address { get; set; }
             public string Password { get; set; }
             public int EmployeeCode { get; set; }
             public string Function { get; set; }
             public float Salary { get; set; }
             public DateTimeOffset HiringDate  { get; set; }
             public string Status  { get; set; }
             public int DepartamentId  { get; set; }
        }

        public class CreateUserCommandHandler: IRequestHandler<CreateEmployeeUserCommand,Result<EmployeeDto>>{
             private readonly IMapper _mapper;
             private readonly UserManager<ApplicationUser> _userManager;
             private readonly  IUnitOfWork _unitOfWork;
             private readonly DataContext _context;

            public CreateUserCommandHandler(IMapper mapper,UserManager<ApplicationUser> userManager, IUnitOfWork unitOfWork,DataContext context){
                 _mapper = mapper;
                _userManager = userManager;
                _unitOfWork = unitOfWork;
                _context = context;
            }

            public async Task<Result<EmployeeDto>> Handle(CreateEmployeeUserCommand request,
                CancellationToken cancellationToken)
            {
              var  employeeList = await _unitOfWork.Repository<Employee>().ListAllAsync();
                var check = await _userManager.FindByEmailAsync(request.Email);
                if (check is not null)
                {
                    return Results.ConflictError("Email Exists");
                }

                await using var transactionScope = await _context.Database.BeginTransactionAsync(cancellationToken);

                try
                {
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
                    var employee = new Employee()
                    {
                        UserId = user.Id, 
                        EmployeeCode = GenerateCode(employeeList),
                        Function = request.Function,
                        Salary = request.Salary,
                        HiringDate = request.HiringDate,
                        Status = request.Status,
                        DepartamentId = request.DepartamentId

                    };
                   
                    _unitOfWork.Repository<Employee>().Add(employee);
                    var result2 = await _unitOfWork.Complete() < 0;
                    if (result2)
                    {
                      return Results.InternalError("Error While saving");
                    }
                    await transactionScope.CommitAsync(cancellationToken);
                    return _mapper.Map<EmployeeDto>(employee);

                }
                return Results.InternalError("Error While saving");
          

            } catch (Exception e)
            {
                await transactionScope.RollbackAsync(cancellationToken);
                throw new Exception(e.Message);
            }


        }
            private static int GenerateCode(IReadOnlyCollection<Employee> employees)
            {
                while (true)
                {
                    var code = new Random().Next(1000, 9999);

                    if (employees.All(x => x.EmployeeCode != code))
                    {
                        return code;
                    }
                }
            }
   


          
        }
    }
