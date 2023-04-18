using Application.Specifications;
using Domain;

namespace Application.Features.Departaments.Specifications;

public class GetDepartamentByNameSpecification:BaseSpecification<Departament>
{
    public GetDepartamentByNameSpecification(string description):base(type=>type.Description.ToLower() == description.ToLower())
    {
    
    }
    
}