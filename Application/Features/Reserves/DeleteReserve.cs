using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Dtos;
using Application.Errors;
using Application.Features.Reserves.Specificationss;
using Application.Interfaces;
using AutoMapper;
using Domain;
using FluentResults;
using MediatR;

namespace Application.Features.Reserves;
public class DeleteReserve
    {
        public class  DeleteReserveCommand:IRequest<Result<ReserveDto>>
        {
            public int Id { get; set; }
            
        }
        
        public class DeleteReserveCommandHandler:IRequestHandler<DeleteReserveCommand,Result<ReserveDto>>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;
            public DeleteReserveCommandHandler(IUnitOfWork unitOfWork,IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;

            }
            public async Task<Result<ReserveDto>> Handle(DeleteReserveCommand request, CancellationToken cancellationToken)
            {
                var reserve = await _unitOfWork.Repository<Reserve>().GetByIdAsync(request.Id);
                if (reserve is null)
                {
                    return Results.NotFoundError("Id: " + request.Id);
                }
                _unitOfWork.Repository<Reserve>().Delete(reserve);
                var resul = await _unitOfWork.Complete();
                if (resul < 0)
                {
                    return Results.InternalError("Reserve don't  deleted");
                }

                return _mapper.Map<ReserveDto>(reserve);

            }
        }
        
    }
