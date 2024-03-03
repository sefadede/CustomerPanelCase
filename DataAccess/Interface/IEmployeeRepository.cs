using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interface
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        Task<bool> ChangeStatus(int employeeId,int statusId);
        Task<Employee> CheckLogin(string Email);
        Employee FindEmployeeByEmail(string Email);
        List<Employee> GetAllByJobType(int jobId);
    }
}
