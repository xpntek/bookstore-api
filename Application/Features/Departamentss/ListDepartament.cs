using Application.Dtos;
using Application.Interfaces;
using AutoMapper;
using Domain;
using FluentResults;
using MediatR;

namespace Application.Features.Departaments;

public class ListDepartament
{
    public class LisDepartamentQuery : IRequest<Result<List<DepartamentDto>>>
    {
        
    }

    public class LisDepartamentQueryHandler : IRequestHandler<LisDepartamentQuery, Result<List<DepartamentDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public LisDepartamentQueryHandler(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;

        }
        public async Task<Result<List<DepartamentDto>>> Handle(LisDepartamentQuery request, CancellationToken cancellationToken)
        {
            var list = await _unitOfWork.Repository<Departament>().ListAllAsync();

            return  _mapper.Map<List<DepartamentDto>>(list);

        }
    }
    
    }