using Application.Dtos;
using Application.Errors;
using Application.Interfaces;
using AutoMapper;
using Domain;
using FluentResults;
using MediatR;

namespace Application.Security.Clients;

public class UpdateClientUser
{
    public class UpdateClientUserCommand:IRequest<Result<ClientdDto>>
    {
        public int Id { get; set; }
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
    public class UpdateClientUserCommandHandler:IRequestHandler<UpdateClientUserCommand,Result<ClientdDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateClientUserCommandHandler(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            
        }
        public async Task<Result<ClientdDto>> Handle(UpdateClientUserCommand request, CancellationToken cancellationToken)
        {
            var check = await _unitOfWork.Repository<Client>().GetByIdAsync(request.Id);
            if (check is null)
            {
                return Results.NotFoundError("Client not found");
            }

            check.ApplicationUser.Email = request.Email;
            check.ApplicationUser.Address = request.Address;
            check.ApplicationUser.Fullname = request.FullName;
            check.ApplicationUser.PhoneNumber = request.PhoneNumber;
            check.ApplicationUser.UserName = request.UserName;
            check.ClientCode = request.ClientCode;
            check.Discount = request.Discount;
            check.ShoppingAmount = request.ShoppingAmount;
           
            _unitOfWork.Repository<Client>().Update(check);
            var resul = await _unitOfWork.Complete();
            if (resul > 0)
            {
                return _mapper.Map<ClientdDto>(check);

            }
            return Results.InternalError("Error While saving");

        }
        }
    }
