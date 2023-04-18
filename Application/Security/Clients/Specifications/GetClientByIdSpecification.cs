using Application.Specifications;
using Domain;

namespace Application.Security.Clients;

public class GetClientByIdSpecification:BaseSpecification<Client>
{
    public GetClientByIdSpecification(int id):base(c=>c.Id==id)
    {
        AddInclude(x=>x.ApplicationUser);
        
    }
    
}