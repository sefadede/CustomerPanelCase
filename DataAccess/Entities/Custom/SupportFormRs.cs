using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Entities.Custom
{
    public class SupportFormRs
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public string NameSurname { get; set; }
        public string CustomerNameSurname { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public DateTime Date { get; set; }
        public int FormStatusId { get; set; }
        public string FromStatusText { get; set; }
    }
}
