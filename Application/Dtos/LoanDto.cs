namespace Application.Dtos;

public class LoanDto
{
    public DateTimeOffset LoanDate { get; set; }
    public DateTimeOffset ReturnDate { get; set; }
    public DateTimeOffset EffectiveReturnDate { get; set; }
    public string Status { get; set; }
    public float Forfeit { get; set; }
    public int BookId { get; set; }
    public string UserId { get; set; }
}