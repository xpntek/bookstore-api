using Application.Dtos;
using Application.Interfaces;
using AutoMapper;
using Domain;
using FluentResults;
using MediatR;

namespace Application.Features.Categories;

public class ListCategory
{
    public class ListCategoryQuery : IRequest<Result<List<CategoryDto>>>
    {
    }

    public class ListCategoryQueryHandler : IRequestHandler<ListCategoryQuery, Result<List<CategoryDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ListCategoryQueryHandler( IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Result<List<CategoryDto>>> Handle(ListCategoryQuery request, CancellationToken cancellationToken)
        {
            var categories = await _unitOfWork.Repository<Category>().ListAllAsync();
            return _mapper.Map<List<CategoryDto>>(categories);
        }
    }
}