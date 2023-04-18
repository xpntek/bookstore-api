using Application.Dtos;
using Application.Errors;
using Application.Interfaces;
using AutoMapper;
using Domain;
using FluentResults;
using MediatR;

namespace Application.Security.Clients;

public class DeleteClientUser
{
    public class DeleteClientUserCommand : IRequest<Result<ClientdDto>>
    {
        public int Id { get; set; }

    }

    public class DeleteClientUserCommandHandler : IRequestHandler<DeleteClientUserCommand, Result<ClientdDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteClientUserCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;

        }

        public async Task<Result<ClientdDto>> Handle(DeleteClientUserCommand request,
            CancellationToken cancellationToken)
        {

            var client = await _unitOfWork.Repository<Client>().GetByIdAsync(request.Id);
            if (client is null)
            {
                return Results.NotFoundError("Id: " + request.Id);
            }

            _unitOfWork.Repository<Client>().Delete(client);
            var resul = await _unitOfWork.Complete();
            if (resul < 0)
            {
                return Results.InternalError("Client don't  deleted");
            }

            return _mapper.Map<ClientdDto>(client);

        }
    }
}