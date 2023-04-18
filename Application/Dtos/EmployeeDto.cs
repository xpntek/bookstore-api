using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public class EmployeeDto
    { 
        public string UserId{get;set;}  
        public string Email { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public int EmployeeCode { get; set; }
        public string Function { get; set; }
        public float Salary { get; set; }
        public DateTimeOffset HiringDate  { get; set; }
        public string Status  { get; set; }
        public string Description  { get; set; }
        
    }
}