using Application.Dtos;
using Application.Errors;
using Application.Features.Categories.Specification;
using Application.Interfaces;
using AutoMapper;
using Domain;
using FluentResults;
using MediatR;

namespace Application.Features.Categories;

public class UpdateCategory
{
    public class UpdateCategoryCommand : IRequest<Result<CategoryDto>>
    {
        public int Id { get; set; }
        public string Description { get; set; }
    }

    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, Result<CategoryDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateCategoryCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<CategoryDto>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _unitOfWork.Repository<Category>().GetByIdAsync(request.Id);
            if (category is null)
            {
                return Results.NotFoundError("" + request.Id);
            }

            category.Description = request.Description.ToLower();

            _unitOfWork.Repository<Category>().Update(category);
            var result = await _unitOfWork.Complete();

            if (result < 0)
            {
                return Results.InternalError(request.Description + " don't update");
            }

            return _mapper.Map<CategoryDto>(category);
        }
    }
}