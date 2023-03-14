using Application.Specifications;
using Domain;

namespace Application.Features.Loans.Specification;

public class FoundLoanByIdSpecification : BaseSpecification<Loan>
{
    public FoundLoanByIdSpecification(int loanId) : base(loan => loan.Id==loanId )
    {
        AddInclude(loan => loan.Book );
        AddInclude(loan => loan.User );
    }
}