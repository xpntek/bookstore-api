using Domain;

namespace Application.Dtos;

public class BookDto
{
    public string Title { get; set; }
    public string PublishingCompany { get; set; }
    public string ISBN { get; set; }
    public string PublishingYear { get; set; }
    public string Edition { get; set; }
    public string PagesNumbers { get; set; }
    public string Category { get; set; }
    public int CategoryId { get; set; }
    public string Synopsis { get; set; }
    public float Price { get; set; }
    public int BookAmount { get; set; }
    public string CoverUrl { get; set; }
    public int Rating { get; set; }
    public List<string> Authors { get; set; } = new List<string>();
}