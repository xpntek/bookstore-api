using Application.Specifications;
using Domain;

namespace Application.Security.Specifications;

public class AllEmployeeSpecification:BaseSpecification<Employee>
{
   public AllEmployeeSpecification():base(){
   
       AddInclude(ac=>ac.Departament);
       AddInclude(ac=>ac.ApplicationUser);
   }

    /*public GetEmployeeByIdSpecification(int id):base(ac => ac.Id == id)
    {
         AddInclude(ac=>ac.Departament);
         AddInclude(ac=>ac.ApplicationUser);
       
    }*/
}