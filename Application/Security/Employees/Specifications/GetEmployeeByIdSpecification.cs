using Application.Specifications;
using Domain;

namespace Application.Security.Specifications;

public class GetEmployeeByIdSpecification:BaseSpecification<Employee>
{
    public GetEmployeeByIdSpecification(int id):base(c=>c.Id==id)
    {
        AddInclude(x=>x.Departament);
        AddInclude(x=>x.ApplicationUser);
        
    }
    
}