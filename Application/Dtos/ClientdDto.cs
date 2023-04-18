namespace Application.Dtos;

public class ClientdDto
{
    public string UserId {get;set;}
    public string Email { get; set; }
    public string FullName { get; set; }
    public string UserName { get; set; }
    public string PhoneNumber { get; set; }
    public string Address { get; set; }
   
    public int ClientCode { get; set; }
    public int ShoppingAmount { get; set; }
    public int Discount { get; set; }
}