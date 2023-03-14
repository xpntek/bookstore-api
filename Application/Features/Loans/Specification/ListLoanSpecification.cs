using Application.Specifications;
using Domain;

namespace Application.Features.Loans.Specification;

public class ListLoanSpecification : BaseSpecification<Loan>
{
    public ListLoanSpecification() : base()
    {
        AddInclude(loan => loan.Book );
        AddInclude(loan => loan.User );
    }
}