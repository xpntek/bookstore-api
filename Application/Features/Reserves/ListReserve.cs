using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Dtos;
using Application.Features.Reserves.Specificationss;
using Application.Interfaces;
using AutoMapper;
using Domain;
using FluentResults;
using MediatR;

namespace Application.Features.Reserves;

    public class ListReserve
    {
        public class ListReserveQuery:IRequest<Result<List<ReserveDto>>>
        {
            
        }
        
        public class ListReserveQueryHandler:IRequestHandler<ListReserveQuery,Result<List<ReserveDto>>>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;
            public ListReserveQueryHandler(IUnitOfWork unitOfWork,IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;

            }
            public async Task<Result<List<ReserveDto>>> Handle(ListReserveQuery request, CancellationToken cancellationToken)
            {
                var reserveSpec = new AllReserveSpecification();
                var check = await _unitOfWork.Repository<Reserve>().ListWithSpecAsync(reserveSpec);
                return _mapper.Map<List<ReserveDto>>(check);
            }
        }
        
    }
