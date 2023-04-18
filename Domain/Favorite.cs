using System.ComponentModel.DataAnnotations.Schema;

namespace Domain;

public class Favorite:BaseEntity
{
    public int BookId { get; set; }
    [ForeignKey("BooKId")] public Book? Book { get; set; }
    public string UserId { get; set; }
    [ForeignKey("UserId")] public ApplicationUser? User { get; set; }
}