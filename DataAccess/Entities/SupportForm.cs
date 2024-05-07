using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Entities
{
    public class SupportForm : EntityBase
    {
        public int EmployeeId { get; set; }
        public int CustomerEmployeeId { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public DateTime Date { get; set; }
        public int FormStatusId { get; set; }
    }
}
