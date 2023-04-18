using Application.Dtos;
using Application.Errors;
using Application.Features.Departaments.Specifications;
using Application.Interfaces;
using AutoMapper;
using FluentResults;
using MediatR;
using Domain;

namespace Application.Features.Departaments;

public class CreateDepartament
{
    public class CreateDepartamentCommand:IRequest<Result<DepartamentDto>>
    {
        public string Description { get; set; }
        
    }
    public class CreateDepartamentCommandHandler:IRequestHandler<CreateDepartamentCommand,Result<DepartamentDto>>
    {
        public readonly IUnitOfWork _UnitOfWork;
        private readonly IMapper _mapper;
        public CreateDepartamentCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _UnitOfWork = unitOfWork;
            _mapper = mapper;

        }
        public async Task<Result<DepartamentDto>> Handle(CreateDepartamentCommand request, CancellationToken cancellationToken)
        {
            var departamentSpec = new GetDepartamentByNameSpecification(request.Description);
            var check = await _UnitOfWork.Repository<Departament>().GetEntityWithSpec(departamentSpec);
            if (check != null)
            {
                Results.ConflictError("Departament Exists");
            }

            var departament = new Departament()
            {
                Description = request.Description
            };
            _UnitOfWork.Repository<Departament>().Add(departament);

            if(await _UnitOfWork.Complete() < 0){
                Results.ConflictError("Error Saving");
            }
            return _mapper.Map<DepartamentDto>(departament);
            

        }
    }

    
}