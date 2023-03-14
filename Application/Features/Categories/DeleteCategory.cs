using Application.Dtos;
using Application.Errors;
using Application.Interfaces;
using AutoMapper;
using Domain;
using FluentResults;
using MediatR;

namespace Application.Features.Categories;

public class DeleteCategory
{
    public class DeleteCategoryCommand : IRequest<Result<CategoryDto>>
    {
        public int Id { get; set; }
    }

    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, Result<CategoryDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteCategoryCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<CategoryDto>> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _unitOfWork.Repository<Category>().GetByIdAsync(request.Id);
            if (category is null)
            {
                return Results.NotFoundError(""+request.Id);
            }
            
            _unitOfWork.Repository<Category>().Delete(category);
           
            var result = await _unitOfWork.Complete();
            if (result < 0)
            {
                return Results.InternalError(request.Id + " don't delete");
            }

            return _mapper.Map<CategoryDto>(category);
        }
    }
}