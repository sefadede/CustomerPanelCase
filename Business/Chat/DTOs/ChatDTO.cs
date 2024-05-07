using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Chat.DTOs
{
    public class ChatDTO
    {
        public List<Employee> EmployeeList { get; set; }
        public int EmployeeId { get; set; }
    }
}
