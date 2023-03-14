using Application.Dtos;
using Application.Errors;
using Application.Features.Loans.Specification;
using Application.Interfaces;
using AutoMapper;
using Domain;
using FluentResults;
using MediatR;

namespace Application.Features.Loans;

public class GetLoanById
{
    public class GetLoanByIdQuery : IRequest<Result<LoanDto>>
    {
        public int Id { get; set; }
    }
    
    public class GetLoanByIdQueryHandler : IRequestHandler<GetLoanByIdQuery,Result<LoanDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetLoanByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Result<LoanDto>> Handle(GetLoanByIdQuery request, CancellationToken cancellationToken)
        {
            var loanSpec = new FoundLoanByIdSpecification(request.Id);
            var loan = await _unitOfWork.Repository<Loan>().GetEntityWithSpec(loanSpec);
            
            if (loan is null)
            {
                Results.NotFoundError("Id:" + request.Id);
            }
            
            return _mapper.Map<LoanDto>(loan);
        }
    }
}