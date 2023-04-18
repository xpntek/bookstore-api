using System.ComponentModel.DataAnnotations.Schema;

namespace Domain;

public class Client:BaseEntity
{
    public int ClientCode { get; set; }
    public int ShoppingAmount { get; set; }
    public int Discount { get; set; }
    public string UserId { get; set; }
    [ForeignKey("UserId")] 
    public ApplicationUser? ApplicationUser { get; set; }
    
}