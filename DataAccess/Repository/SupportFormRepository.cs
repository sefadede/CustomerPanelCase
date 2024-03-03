using Dapper;
using DataAccess.Entities;
using DataAccess.Entities.Custom;
using DataAccess.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class SupportFormRepository : DBContext<SupportForm>, ISupportFormRepository
    {
        public SupportFormRepository(string connectionString)
            : base(connectionString)
        {
        }
        public IEnumerable<SupportForm> GetAll()
        {
            return base.GetAllBase();
        }
        public SupportForm Find(int id)
        {
            return base.FindBase(id);
        }
        public SupportForm Insert(SupportForm entity)
        {
            return base.InsertBase(entity);
        }
        public void Update(SupportForm entity)
        {
            base.UpdateBase(entity);
        }
        public void Delete(int id)
        {
            base.DeleteBase(id);
        }
        public List<SupportFormRs> GetAllByEmployeeId(int employeeId, bool allList)
        {
            List<SupportFormRs> result;
            var sb = new StringBuilder();
            sb.Append("SELECT ");
            sb.Append("sf.Id AS Id, ");
            sb.Append("sf.EmployeeId AS EmployeeId, ");
            sb.Append("CONCAT(e.Name, ' ', e.Surname) AS NameSurname, ");
            sb.Append("sf.Subject AS Subject, ");
            sb.Append("sf.Message AS Message, ");
            sb.Append("sf.Date AS Date, ");
            sb.Append("sf.FormStatusId AS FormStatusId ");
            sb.Append("FROM [dbo].[SupportForm] AS sf ");
            sb.Append("INNER JOIN  [dbo].[Employee] AS e ON sf.EmployeeId = e.Id ");

            if (!allList)
            {
                sb.Append("where sf.EmployeeId = @EmployeeId");
            }

            using (var connection = GetConnection())
            {
                connection.Open();
                result = connection.Query<SupportFormRs>(sb.ToString(), new { EmployeeId = employeeId }).ToList();
            }
            return result;
        }
        public async Task<bool> ChangeStatus(int id, int statusId)
        {
            bool success = true;
            var sql = "update [dbo].[SupportForm] set FormStatusId=@statusId where Id = @id";
            var parameters = new DynamicParameters();
            parameters.Add("statusId", statusId);
            parameters.Add("id", id);

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
