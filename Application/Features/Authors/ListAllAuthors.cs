using Application.Dtos;
using Application.Interfaces;
using AutoMapper;
using Domain;
using FluentResults;
using MediatR;

namespace Application.Features.Authors;

public class ListAllAuthors
{
    public  class ListAllAuthorsQuery: IRequest<Result<List<AuthorDto>>>
    {
        
    }
    
    public class  ListAllAuthorsQueryHandler : IRequestHandler<ListAllAuthorsQuery,Result<List<AuthorDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ListAllAuthorsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Result<List<AuthorDto>>> Handle(ListAllAuthorsQuery request, CancellationToken cancellationToken)
        {
            var authors = await _unitOfWork.Repository<Author>().ListAllAsync();
            return _mapper.Map<List<AuthorDto>>(authors);
        }
    }
}