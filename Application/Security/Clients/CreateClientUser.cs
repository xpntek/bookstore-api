using Application.Dtos;
using Application.Errors;
using Application.Interfaces;
using AutoMapper;
using Domain;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Persistence;

namespace Application.Security.Clients;

public class CreateClientUser
{

  
        public class CreateClientUserCommand : IRequest<Result<ClientdDto>>
        {
            public string Email { get; set; }
            public string FullName { get; set; }
            public string UserName { get; set; }
            public string PhoneNumber { get; set; }
            public string Address { get; set; }
            public string Password { get; set; }
            public int ClientCode { get; set; }
            public int ShoppingAmount { get; set; }
            public int Discount { get; set; }
        }

        public class CreateClientUserCommandHandler : IRequestHandler<CreateClientUserCommand, Result<ClientdDto>>
        {
            private readonly IMapper _mapper;
            private readonly UserManager<ApplicationUser> _userManager;
            private readonly IUnitOfWork _unitOfWork;
            private readonly DataContext _context;

            public CreateClientUserCommandHandler(IMapper mapper, UserManager<ApplicationUser> userManager,
                IUnitOfWork unitOfWork, DataContext context)
            {
                _mapper = mapper;
                _userManager = userManager;
                _unitOfWork = unitOfWork;
                _context = context;
            }

            public async Task<Result<ClientdDto>> Handle(CreateClientUserCommand request,
                CancellationToken cancellationToken)
            {
                var clientList = await _unitOfWork.Repository<Client>().ListAllAsync();
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
                        var client = new Client()
                        {
                            UserId = user.Id,
                            ClientCode = GenerateCode(clientList),
                            ShoppingAmount = request.ShoppingAmount,
                            Discount = request.Discount

                        };
                        _unitOfWork.Repository<Client>().Add(client);
                        var result2 = await _unitOfWork.Complete() < 0;
                        if (result2)
                        {
                            return Results.InternalError("Error While saving");
                        }

                        await transactionScope.CommitAsync(cancellationToken);
                        return _mapper.Map<ClientdDto>(client);

                    }

                    return Results.InternalError("Error While saving");


                }
                catch (Exception e)
                {
                    await transactionScope.RollbackAsync(cancellationToken);
                    throw new Exception(e.Message);
                }


            }

            private static int GenerateCode(IReadOnlyCollection<Client> employees)
            {
                while (true)
                {
                    var code = new Random().Next(1000, 9999);

                    if (employees.All(x => x.ClientCode != code))
                    {
                        return code;
                    }
                }
            }




        }
    }

