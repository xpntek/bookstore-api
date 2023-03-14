using Application.Specifications;
using Domain;

namespace Application.Features.Favorites;

public class FoundClientByIdSpecification : BaseSpecification<Client>
{
    public FoundClientByIdSpecification( string userId) : base( client => client.ApplicationUser.Id == userId )
    {
        AddInclude(client => client.ApplicationUser );
    }
}