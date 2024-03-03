using DataAccess.Entities.Custom;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.SupportForm.DTOs
{
    public class SupportFormDTO
    {
        public List<SupportFormRs> SupportFormRs { get; set; }
        public bool CanEdit { get; set; }
    }
}
