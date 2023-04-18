using Application.Dtos;
using Application.Interfaces;
using AutoMapper;
using Domain;
using FluentResults;
using MediatR;

namespace Application.Security.Clients;

public class ListClientUser
{
    public class ListClientUserQuery:IRequest<Result<List<ClientdDto>>>
    {
        
    }
    public class ListClientUserQueryHandler:IRequestHandler<ListClientUserQuery,Result<List<ClientdDto>>>
    {
        private readonly IUnitOfWork _iUnitOfWork;
        private readonly IMapper _mapper;
       
        public ListClientUserQueryHandler(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _iUnitOfWork = unitOfWork;
            _mapper = mapper;
           

        }
        public async Task<Result<List<ClientdDto>>> Handle(ListClientUserQuery request, CancellationToken cancellationToken)
        {
            var client = new AllClientSpecification();
            var list = await _iUnitOfWork.Repository<Client>().ListWithSpecAsync(client);
          
            return _mapper.Map<List<ClientdDto>>(list);

        }
    }
}