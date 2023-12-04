using System.ComponentModel.DataAnnotations.Schema;

namespace Domain;

public class Employee
{
    public int EmployeeCode { get; set; }
    public string Function { get; set; }
    public float Salary { get; set; }
    public DateTimeOffset HiringDate  { get; set; }
    public string Status  { get; set; }
    public string Department  { get; set; }
    public string UserId { get; set; }
    [ForeignKey("UserId")] 
    public ApplicationUser? ApplicationUser { get; set; }
    
}