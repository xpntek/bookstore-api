namespace Domain;

public class Author: BaseEntity
{
    public string FullName { get; set; }
    public string Nationality { get; set; }
    public string Gender { get; set; }
    public string Biography { get; set; }
}