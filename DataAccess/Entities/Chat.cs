using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Entities
{
    public class Chat:EntityBase
    {
        public int SenderEmployeeId { get; set; }
        public int ReceiverEmployeeId { get; set; }
        public DateTime MessageDate { get; set; }
        public string Subject { get; set; }
        public bool IsRead { get; set; }
    }
}
