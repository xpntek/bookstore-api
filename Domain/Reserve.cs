using System.ComponentModel.DataAnnotations.Schema;

namespace Domain;

public class Reserve : BaseEntity
{
    public DateTimeOffset BookingDate { get; set; }
    public DateTimeOffset EndDate { get; set; }
    public int BookId { get; set; }
    [ForeignKey("BooKId")] public Book? Book { get; set; }
    public int UserId { get; set; }
    [ForeignKey("UserId")] public ApplicationUser? User { get; set; }
    public string Status { get; set; }
}