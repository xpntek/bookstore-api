using Application.Dtos;
using Application.Errors;
using Application.Features.Categories.Specification;
using Application.Interfaces;
using AutoMapper;
using Domain;
using FluentResults;
using MediatR;

namespace Application.Features.Categories;

public class CreateCategory
{
    public class CreateCategoryCommand : IRequest<Result<CategoryDto>>
    {
        public string Description { get; set; }
    }

    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, Result<CategoryDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateCategoryCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<CategoryDto>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var categorySpec = new FoundCategoryByDescriptionSpecification(request.Description);
            var category = await _unitOfWork.Repository<Category>().GetEntityWithSpec(categorySpec);
            if (category is not null)
            {
                return Results.ConflictError(request.Description);
            }

            var newCategory = new Category()
            {
                Description = request.Description.ToLower()
            };

            _unitOfWork.Repository<Category>().Add(newCategory);
            var result = await _unitOfWork.Complete();
            if (result < 0)
            {
                return Results.InternalError(request.Description + " not save");
            }

            return _mapper.Map<CategoryDto>(newCategory);
            
        }
    }
}