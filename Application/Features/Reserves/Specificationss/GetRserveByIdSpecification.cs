using Application.Specifications;
using Domain;

namespace Application.Features.Reserves.Specificationss;

public class GetRserveByIdSpecification:BaseSpecification<Reserve>
{
    public GetRserveByIdSpecification(int id):base (reserve=>reserve.Id == id)
    {
        AddInclude(x=>x.Book);
        AddInclude(x=>x.User);
        
    }
    
}