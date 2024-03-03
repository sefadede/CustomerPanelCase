using DataAccess.Entities;
using DataAccess.Entities.Custom;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interface
{
    public interface ISupportFormRepository : IRepository<SupportForm>
    {
        Task<bool> ChangeStatus(int id, int statusId);
        List<SupportFormRs> GetAllByEmployeeId(int employeeId,bool allList);
    }
}
