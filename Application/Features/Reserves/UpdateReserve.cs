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
using Microsoft.AspNetCore.Identity;

namespace Application.Features.Reserves
{
    public class UpdateReserve
    {
        public class UpdateReserveCommand:IRequest<Result<ReserveDto>>
        {
            public int Id { get; set; }
            public DateTimeOffset BookingDate { get; set; }
            public DateTimeOffset EndDate { get; set; }
            public int BookId { get; set; }
            public string UserId { get; set; }
            public string Status { get; set; }
            
        }

        public class UpdateReserveCommandHandler:IRequestHandler<UpdateReserveCommand, Result<ReserveDto>>
        {
            private readonly UserManager<ApplicationUser> _userManager;
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;

            public UpdateReserveCommandHandler(UserManager<ApplicationUser> userManager, IUnitOfWork unitOfWork, IMapper mapper)
            {
                _userManager = userManager;
                _unitOfWork = unitOfWork;
                _mapper = mapper;

            }
            public async Task<Result<ReserveDto>> Handle(UpdateReserveCommand request, CancellationToken cancellationToken)
            {
               
                var reserveSpec = new GetRserveByIdSpecification(request.Id);
                var check = await _unitOfWork.Repository<Reserve>().GetEntityWithSpec(reserveSpec);
                if (check is null)
                {
                    return Results.ConflictError(""+request.Id);
                
                }

                var user = await _userManager.FindByIdAsync(request.UserId);
                if (user is null)
                {
                    return  Results.NotFoundError(""+request.UserId);
                    
                }

                var book = await _unitOfWork.Repository<Book>().GetByIdAsync(request.BookId);

                if (book is null)
                {
                    return  Results.NotFoundError(""+request.BookId);
                }
                

                check.BookingDate = request.BookingDate;
                check.EndDate = request.EndDate;
                check.Book.Id = request.BookId;
                check.User.Id = request.UserId;
                check.Status = request.Status;
                
                _unitOfWork.Repository<Reserve>().Update(check);
                var resul = await _unitOfWork.Complete();
                if (resul > 0)
                {
                    return _mapper.Map<ReserveDto>(check);

                }
                return Results.InternalError("Error While saving");

            }
        }
        
    }
}