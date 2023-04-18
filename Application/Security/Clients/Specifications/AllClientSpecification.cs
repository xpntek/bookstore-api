using Application.Specifications;
using Domain;

namespace Application.Security.Clients;

public class AllClientSpecification:BaseSpecification<Client>
{
    public AllClientSpecification():base()
    {
        AddInclude(c=>c.ApplicationUser);
        
    }
    
}