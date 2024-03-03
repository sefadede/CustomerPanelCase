using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Account.DTOs
{
    public class LoginDTO
    {
        public Employee Employee { get; set; }
        public bool Success { get; set; }
        public string ReturnUrl { get; set; }
    }
}
