using Dapper;
using DataAccess.Entities;
using DataAccess.Interface;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class EmployeeRepository : DBContext<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(string connectionString)
            : base(connectionString)
        {
        }
        public IEnumerable<Employee> GetAll()
        {
            return base.GetAllBase();
        }
        public Employee Find(int id)
        {
            return base.FindBase(id);
        }
        public Employee Insert(Employee entity)
        {
            return base.InsertBase(entity);
        }
        public void Update(Employee entity)
        {
            base.UpdateBase(entity);
        }
        public void Delete(int id)
        {
            base.DeleteBase(id);
        }
        public async Task<Employee> CheckLogin(string Email)
        {
            Employee result = null;

            try
            {
                var sb = new StringBuilder();
                sb.Append("select * from [dbo].[Employee] ");
                sb.Append("where Email = @Email");
                using (var connection = GetConnection())
                {
                    connection.Open();
                    result = await connection.QueryFirstOrDefaultAsync<Employee>(sb.ToString(),
                          new { Email = Email }, null, null, CommandType.Text);
                }
                sb.Clear();
            }
            catch (Exception ex)
            {

                throw;
            }

            return result;
        }


        public List<Employee> GetAllByStatusId(int statusId)
        {
            List<Employee> result;
            var sb = new StringBuilder();
            sb.Append("select * from [dbo].[Employee] ");
            sb.Append("where StatusId = @StatusId");

            using (var connection = GetConnection())
            {
                connection.Open();
                result = connection.Query<Employee>(sb.ToString(), new {StatusId = statusId }).ToList();
            }
            return result;
        }
        public List<Employee> GetAllByJobType(int jobId)
        {
            List<Employee> result;
            var sb = new StringBuilder();
            sb.Append("select * from [dbo].[Employee] ");
            sb.Append("where JobId = @JobId ");
            sb.Append("and StatusId = @StatusId");

            using (var connection = GetConnection())
            {
                connection.Open();
                result = connection.Query<Employee>(sb.ToString(), new { JobId = jobId, StatusId = (int)Enum.EmployeeStatus.Approved }).ToList();
            }
            return result;
        }
        public Employee FindEmployeeByEmail(string Email)
        {
            Employee emp = null;
            var sql = "select *from [dbo].[Employee] where Email = @Email";
            var parameters = new DynamicParameters();
            parameters.Add("Email", Email);
            using (var connection = GetConnection())
            {
                connection.Open();
                emp = connection.QueryFirstOrDefault<Employee>(sql, parameters);
            }
            return emp;
        }
        public async Task<bool> ChangeStatus(int employeeId, int statusId)
        {
            bool success = true;
            var sql = "update [dbo].[Employee] set StatusId=@statusId where Id = @employeeId";
            var parameters = new DynamicParameters();
            parameters.Add("statusId", statusId);
            parameters.Add("employeeId", employeeId);

            using (var connection = GetConnection())
            {
                try
                {
                    connection.Open();
                    await connection.ExecuteAsync(sql, parameters, null, null, System.Data.CommandType.Text);
                }
                catch { success = false; }
            }
            return success;
        }
    }
}
