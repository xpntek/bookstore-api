using Application.Dtos;
using Application.Errors;
using Application.Interfaces;
using AutoMapper;
using Domain;
using FluentResults;
using MediatR;

namespace Application.Features.Loans;

public class DeleteLoan
{
    public class DeleteLoanCommand : IRequest<Result<LoanDto>>
    {
        public int Id { get; set; }
    }

    public class DeleteLoanCommandHandler : IRequestHandler<DeleteLoanCommand, Result<LoanDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteLoanCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<LoanDto>> Handle(DeleteLoanCommand request, CancellationToken cancellationToken)
        {
            var loan = await _unitOfWork.Repository<Loan>().GetByIdAsync(request.Id);
            if (loan is null)
            {
                return Results.NotFoundError("Id:" + request.Id);
            }

            _unitOfWork.Repository<Loan>().Delete(loan);
            var result = await _unitOfWork.Complete();
            if (result < 0)
            {
                return Results.InternalError("Error to delete");
            }

            return _mapper.Map<LoanDto>(loan);
        }
    }
}