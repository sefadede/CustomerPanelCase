using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Account.DTOs
{
    public class EmployeeListDTO
    {
        public List<Employee> Employee { get; set; }
        public int CanEdit { get; set; }
        public string Jwt { get; set; }
    }
}
