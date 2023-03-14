using Application.Dtos;
using Application.Errors;
using Application.Features.Categories.Specification;
using Application.Interfaces;
using AutoMapper;
using Domain;
using FluentResults;
using MediatR;

namespace Application.Features.Categories;

public class ListCategoryByDescription
{
    public class ListCategoryByDescriptionQuery : IRequest<Result<CategoryDto>>
    {
        public string Description { get; set; }
    }

    public class ListCategoryByDescriptionQueryHandler : IRequestHandler<ListCategoryByDescriptionQuery, Result<CategoryDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ListCategoryByDescriptionQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<CategoryDto>> Handle(ListCategoryByDescriptionQuery request,
            CancellationToken cancellationToken)
        {
            var categorySpec = new FoundCategoryByDescriptionSpecification(request.Description);
            var category = await _unitOfWork.Repository<Category>().GetEntityWithSpec(categorySpec);
            if (category is null)
            {
                return Results.NotFoundError("Description: " + request.Description);
            }

            return _mapper.Map<CategoryDto>(category);
        }
    }
}