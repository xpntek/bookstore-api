using Application.Specifications;
using Domain;

namespace Application.Features.Reserves.Specificationss;

public class AllReserveSpecification:BaseSpecification<Reserve>
{
    public AllReserveSpecification()
    {
        AddInclude(x=>x.Book);
        AddInclude(x=>x.User);
        
    }
    
}