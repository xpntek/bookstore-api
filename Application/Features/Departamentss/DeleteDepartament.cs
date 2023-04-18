using Application.Dtos;
using Application.Errors;
using Application.Interfaces;
using AutoMapper;
using Domain;
using FluentResults;
using MediatR;

namespace Application.Features.Departaments;

public class DeleteDepartament
{
    public class DeleteDeartamentCommand:IRequest<Result<DepartamentDto>>

    {
        public int Id { get; set; }
        public string Description { get; set; }

    }

    public class DeleteDeartamentCommandHandler : IRequestHandler<DeleteDeartamentCommand, Result<DepartamentDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public DeleteDeartamentCommandHandler(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;


        }
        public async Task<Result<DepartamentDto>> Handle(DeleteDeartamentCommand request, CancellationToken cancellationToken)
        {
            var check = await _unitOfWork.Repository<Departament>().GetByIdAsync(request.Id);
            if (check is null)
            {
                return Results.NotFoundError("Id: " + request.Id);
            }
            _unitOfWork.Repository<Departament>().Delete(check);
            var resul = await _unitOfWork.Complete();
            if (resul < 0)
            {
                return Results.InternalError("Departament don't  deleted");
            }

            return _mapper.Map<DepartamentDto>(check);
        }
    }

}