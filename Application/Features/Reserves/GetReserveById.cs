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
public class GetReserveById
    {
        public class GetReserveByIdQuery:IRequest<Result<ReserveDto>>
        {
            public int Id;

        }
        public class GetReserveByIdQueryHandler:IRequestHandler<GetReserveByIdQuery,Result<ReserveDto>>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;
            public GetReserveByIdQueryHandler(IUnitOfWork unitOfWork,IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;

            }
            public async Task<Result<ReserveDto>> Handle(GetReserveByIdQuery request, CancellationToken cancellationToken)
            {
                var reserveSpec = new GetRserveByIdSpecification(request.Id);
                var check = await _unitOfWork.Repository<Reserve>().GetEntityWithSpec(reserveSpec);
                if (check is null)
                {
                    return Results.ConflictError(""+request.Id);
                
                }

                return _mapper.Map<ReserveDto>(check);
            }
        }
        
        
    }
