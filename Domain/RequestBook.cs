using System.ComponentModel.DataAnnotations.Schema;

namespace Domain;

public class RequestBook:BaseEntity
{
    public int RequestId { get; set; }
    [ForeignKey("RequestId")] public Request Request { get; set; }
  
    public int BookId { get; set; }
    [ForeignKey("BookId")] public Book Book { get; set; }
}