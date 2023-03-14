using Application.Dtos;
using Application.Errors;
using Application.Interfaces;
using AutoMapper;
using Domain;
using FluentResults;
using MediatR;

namespace Application.Features.Categories;

public class ListCategoryById
{
    public class ListCategoryByIdQuery :IRequest<Result<CategoryDto>>
    {
        public int Id { get; set; }
    }
    public class ListCategoryByIdQueryHandler : IRequestHandler<ListCategoryByIdQuery,Result<CategoryDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ListCategoryByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Result<CategoryDto>> Handle(ListCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var category = await _unitOfWork.Repository<Category>().GetByIdAsync(request.Id);
            if (category is null)
            {
                return Results.NotFoundError("Id: "+request.Id);
            }

            return _mapper.Map<CategoryDto>(category);
        }
    }
    
}