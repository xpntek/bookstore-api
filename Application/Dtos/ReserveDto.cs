using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public class ReserveDto
    {
    public DateTimeOffset BookingDate { get; set; }
    public DateTimeOffset EndDate { get; set; }
    public int BookId { get; set; }
    public string UserId { get; set; }
    public string Status { get; set; }
    }
}