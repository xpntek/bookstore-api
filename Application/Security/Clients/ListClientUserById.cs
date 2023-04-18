using Application.Dtos;
using Application.Errors;
using Application.Interfaces;
using AutoMapper;
using Domain;
using FluentResults;
using MediatR;

namespace Application.Security.Clients;

public class ListClientUserById
{
    public class ListClientUserByIdIdQuery:IRequest<Result<ClientdDto>>
    {
        public int Id { get; set; }
    }
    public class ListClientUserByIdQueryHandler:IRequestHandler<ListClientUserByIdIdQuery,Result<ClientdDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ListClientUserByIdQueryHandler(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;

        }
        public async Task<Result<ClientdDto>> Handle(ListClientUserByIdIdQuery request, CancellationToken cancellationToken)
        {
            var client = new GetClientByIdSpecification(request.Id);
            var check = await _unitOfWork.Repository<Client>().GetEntityWithSpec(client);
            if (check is null)
            {
                return Results.ConflictError(""+request.Id);
                
            }

            return _mapper.Map<ClientdDto>(check);

        }
    }
}