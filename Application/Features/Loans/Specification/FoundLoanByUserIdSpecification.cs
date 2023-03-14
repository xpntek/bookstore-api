using Application.Specifications;
using Domain;

namespace Application.Features.Loans.Specification;

public class FoundLoanByUserIdSpecification : BaseSpecification<Loan>
{ 
    public FoundLoanByUserIdSpecification( string userId) : base( loan =>loan.User.Id==userId )
    {
        AddInclude(loan => loan.Book );
        AddInclude(loan => loan.User );
    }
}