﻿using Application.Dtos;
using Application.Errors;
using Application.Features.Loans.Specification;
using Application.Interfaces;
using AutoMapper;
using Domain;
using FluentResults;
using MediatR;

namespace Application.Features.Loans;

public class UpdateLoan
{
    public class UpdateLoanCommand : IRequest<Result<LoanDto>>
    {
        public int Id { get; set; }
        public string status { get; set; }
        public float Forfeit { get; set; }
        public DateTimeOffset? EffectiveReturnDate { get; set; }
    }

    public class UpdateLoanCommandHandler : IRequestHandler<UpdateLoanCommand, Result<LoanDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateLoanCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<LoanDto>> Handle(UpdateLoanCommand request, CancellationToken cancellationToken)
        {
            var loanSpec = new GetLoanByIdSpecification(request.Id);
            var loan = await _unitOfWork.Repository<Loan>().GetEntityWithSpec(loanSpec);
            if (loan is null)
            {
                return Results.NotFoundError("Id:" + request.Id);
            }

            loan.Status = request.status;
            loan.Forfeit = request.Forfeit;
            loan.EffectiveReturnDate = request.EffectiveReturnDate;
            
            _unitOfWork.Repository<Loan>().Update(loan);
            var result = await _unitOfWork.Complete();
            if (result < 0)
            {
                return Results.InternalError("Error to update");
            }

            return _mapper.Map<LoanDto>(loan);
        }
    }
}