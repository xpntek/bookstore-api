using System.ComponentModel.DataAnnotations.Schema;

namespace Domain;

public class Loan:BaseEntity
{
    public DateTimeOffset LoanDate { get; set; }
    public DateTimeOffset ReturnDate { get; set; }
    public DateTimeOffset EffectiveReturnDate { get; set; }
    public string Status { get; set; }
    public float Forfeit { get; set; }
    public int BookId { get; set; }
    [ForeignKey("BooKId")] public Book? Book { get; set; }
    public string UserId { get; set; }
    [ForeignKey("UserId")] public ApplicationUser? User { get; set; }
}