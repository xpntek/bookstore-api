using Application.Dtos;
using Application.Errors;
using Application.Interfaces;
using AutoMapper;
using Domain;
using FluentResults;
using MediatR;

namespace Application.Features.Departaments;

public class UpdateDepartament
{
    public class UpdateDepartamentCommand : IRequest<Result<DepartamentDto>>
    {
        public int Id { get; set; }
        public string Description { get; set; }
    }
    public class UpdateDepartamentCommandHandler:IRequestHandler<UpdateDepartamentCommand,Result<DepartamentDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UpdateDepartamentCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            
        }
        public async Task<Result<DepartamentDto>> Handle(UpdateDepartamentCommand request, CancellationToken cancellationToken)
        {
            var check = await _unitOfWork.Repository<Departament>().GetByIdAsync(request.Id);
            if (check is null)
            {
                return Results.NotFoundError("Id: " + request.Id);
            }

             check.Description = request.Description;
            _unitOfWork.Repository<Departament>().Update(check);
            var result = await _unitOfWork.Complete();
             if (result < 0)
             {
             return Results.InternalError("Departament don't save");
             }
            
                        return _mapper.Map<DepartamentDto>(check);
        }
    }
}