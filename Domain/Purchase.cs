using System.ComponentModel.DataAnnotations.Schema;

namespace Domain;

public class Purchase : BaseEntity
{
    public DateTimeOffset PurchaseDate { get; set; }
    public int BookId { get; set; }
    [ForeignKey("BooKId")] public Book? Book { get; set; }
    public int UserId { get; set; }
    [ForeignKey("UserId")] public ApplicationUser? User { get; set; }
    public int BookAmount { get; set; }
    public float TotalValue { get; set; }
    public string PaymentMethod { get; set; }
    public string Status { get; set; }
}