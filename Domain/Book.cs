using System.ComponentModel.DataAnnotations.Schema;

namespace Domain;

public class Book: BaseEntity
{
    public string Title { get; set; }
    public string Author { get; set; }
    public string PublishingCompany { get; set; }
    public string ISBN { get; set; }
    public string PublishingYear { get; set; }
    public string Edition { get; set; }
    public string PagesNumbers { get; set; }
    public int CategoryId  { get; set; }
    [ForeignKey("CategoryId")]
    public Category Category { get; set; }
    public string Synopsis { get; set; }
    public float Price { get; set; }
    public int BookAmount { get; set; }
    public string CoverUrl { get; set; }
    public int Rating { get; set; }
    
    
}