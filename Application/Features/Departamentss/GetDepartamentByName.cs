using Application.Dtos;
using Application.Errors;
using Application.Features.Departaments.Specifications;
using Application.Interfaces;
using AutoMapper;
using Domain;
using FluentResults;
using MediatR;

namespace Application.Features.Departaments;

public class GetDepartamentByName
{
    public class GetDepartamentByNameQuery : IRequest<Result<DepartamentDto>>
    {
        public string Description { get; set; }
    }

    public class GetDepartamentByNameQueryHandler : IRequestHandler<GetDepartamentByNameQuery, Result<DepartamentDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetDepartamentByNameQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            
        }
        public async Task<Result<DepartamentDto>> Handle(GetDepartamentByNameQuery request, CancellationToken cancellationToken)
        {
            var departamentSpec = new GetDepartamentByNameSpecification(request.Description);
            var departament = await _unitOfWork.Repository<Departament>().GetEntityWithSpec(departamentSpec);
            if (departament is null)
            {
                return Results.NotFoundError(" Not found departament name ");
            }
            return _mapper.Map<DepartamentDto>(departament);
        }
    }
}