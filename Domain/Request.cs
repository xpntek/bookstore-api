using System.ComponentModel.DataAnnotations.Schema;

namespace Domain;

public class Request:BaseEntity
{
    public DateTimeOffset RequestDate { get; set; }
    public DateTimeOffset ExpectedDeliveryDate { get; set; }
    public string EmployeeId { get; set; }
    [ForeignKey("EmployeeId")] 
    public Employee Employee { get; set; }
    public string Status { get; set; }
    public string Comment { get; set; }
}