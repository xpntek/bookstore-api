using Application.Dtos;
using Application.Features.Loans.Specification;
using Application.Interfaces;
using AutoMapper;
using Domain;
using FluentResults;
using MediatR;

namespace Application.Features.Loans;

public class ListLoan
{
    public class ListLoanQuery : IRequest<Result<List<LoanDto>>>
    {
    }

    public class ListLoanQueryHandler : IRequestHandler<ListLoanQuery, Result<List<LoanDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ListLoanQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<List<LoanDto>>> Handle(ListLoanQuery request, CancellationToken cancellationToken)
        {
            var loanSpec = new ListLoanSpecification();
            var loanList = await _unitOfWork.Repository<Loan>().ListWithSpecAsync(loanSpec);
            return _mapper.Map<List<LoanDto>>(loanList);
        }
    }
}