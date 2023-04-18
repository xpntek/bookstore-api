using Application.Dtos;
using Application.Errors;
using Application.Interfaces;
using AutoMapper;
using Domain;
using FluentResults;
using MediatR;

namespace Application.Features.Authors;

public class ListAuthorById
{
    
    public  class ListAuthorByIdQuery: IRequest<Result<AuthorDto>>
    {
        public int Id { get; set; }
    }
    
    public class  ListAuthorByIdQueryHandler : IRequestHandler<ListAuthorByIdQuery,Result<AuthorDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ListAuthorByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        
        public  async  Task<Result<AuthorDto>> Handle(ListAuthorByIdQuery request, CancellationToken cancellationToken)
        {
            var authors = await _unitOfWork.Repository<Author>().GetByIdAsync(request.Id);
            if (authors is null)
            {
                return Results.NotFoundError("Id: " + request.Id);
            }
            return _mapper.Map<AuthorDto>(authors);
        }
    }
}