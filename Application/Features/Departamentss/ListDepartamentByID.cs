using Application.Dtos;
using Application.Errors;
using Application.Interfaces;
using AutoMapper;
using Domain;
using FluentResults;
using MediatR;

namespace Application.Features.Departaments;

public class ListDepartamentByID
{
    public class ListDepartamentByIDQuery : IRequest<Result<DepartamentDto>>
    {
        public int Id { get; set; }
    }
    public class ListDepartamentByIDQueryHandler:IRequestHandler<ListDepartamentByIDQuery,Result<DepartamentDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ListDepartamentByIDQueryHandler(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;

        }
        public async Task<Result<DepartamentDto>> Handle(ListDepartamentByIDQuery request, CancellationToken cancellationToken)
        {
            var check = await _unitOfWork.Repository<Departament>().GetByIdAsync(request.Id);
            if (check is null)
            {
                return Results.NotFoundError("Id: " + request.Id);

            }

            return _mapper.Map<DepartamentDto>(check);
        }
    }
}