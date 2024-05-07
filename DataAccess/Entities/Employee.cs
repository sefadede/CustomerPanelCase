using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Entities
{
    public class Employee : EntityBase
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int JobId { get; set; }
        public int StatusId { get; set; }
        public int MasterEmployeeId { get; set; }
        public string Gender { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
