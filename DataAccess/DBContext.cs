using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DataAccess
{
    public abstract class DBContext<T> where T : EntityBase
    {
        private string _connectionString;
        public const int DefaultTimeoutCache = 5;
        public const int ShortTimeoutCache = 3;

        public DBContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected string TableName => typeof(T).Name;

        public IDbConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }

        public IEnumerable<T> GetAllBase()
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                var result = connection.Query<T>(GenerateGetAllCommand());
                return result;
            }
        }

        public T FindBase(int id)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                var result = connection.Query<T>(GenerateFindCommand(id), FindCommandParameters(id));
                return result.FirstOrDefault();
            }
        }
        public T InsertBase(T entity)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                var id = connection.Query<int>(GenerateInsertCommand(entity), InsertCommandParameters(entity)).Single();
                entity.Id = id;
                return entity;
            }
        }

        public void UpdateBase(T entity)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                connection.Execute(GenerateUpdateCommand(entity), UpdateCommandParameters(entity));
            }
        }

        public void DeleteBase(int id)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                connection.Execute(GenerateDeleteCommand(id), DeleteCommandParameters(id));
            }
        }
        public void BulkInsert(DataTable dt)
        {
            using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(_connectionString))
            {
                sqlBulkCopy.DestinationTableName = dt.TableName;
                sqlBulkCopy.BulkCopyTimeout = 5000;
                sqlBulkCopy.WriteToServer(dt);
                sqlBulkCopy.Close();
            }
        }
        public string FormatSqlParam(string val) { return val.Replace("'", "''").Trim(); }
        private string GenerateGetAllCommand()
        {
            return "SELECT * FROM " + TableName;
        }
        private string GenerateFindCommand(int id)
        {
            return "SELECT * FROM " + TableName + " WHERE Id=@Id";
        }

        private string GenerateInsertCommand(T entity)
        {
            StringBuilder sBuilderColumns = new StringBuilder();
            StringBuilder sBuilderParams = new StringBuilder();
            var properties = typeof(T).GetProperties();

            sBuilderColumns.Append("(");
            sBuilderParams.Append("(");

            foreach (var property in properties)
            {
                if (property.Name == "Id")
                    continue;

                sBuilderColumns.Append("[" + property.Name + "],");
                sBuilderParams.Append("@" + property.Name + ",");
            }

            var columnText = sBuilderColumns.ToString().TrimEnd(',') + ")";
            var paramsText = sBuilderParams.ToString().TrimEnd(',') + ")";

            return "INSERT INTO " + TableName + " " + columnText + " VALUES " + paramsText + " SELECT SCOPE_IDENTITY()";

        }
        private string GenerateUpdateCommand(T entity)
        {
            StringBuilder sBuilder = new StringBuilder();
            var properties = typeof(T).GetProperties();
            sBuilder.Append("UPDATE " + TableName + " SET ");
            foreach (var property in properties)
            {
                if (property.Name == "Id")
                    continue;

                sBuilder.Append("[" + property.Name + "]" + "=@" + property.Name + ",");
            }


            return sBuilder.ToString().TrimEnd(',') + " WHERE Id=@Id";
        }
        private string GenerateDeleteCommand(int id)
        {
            return "DELETE " + TableName + " WHERE Id=@Id";
        }
        protected object FindCommandParameters(int id)
        {
            return new
            {
                @Id = id
            };
        }
        protected virtual object InsertCommandParameters(T entity)
        {
            var parameters = new DynamicParameters();
            var properties = typeof(T).GetProperties();
            foreach (var property in properties)
            {
                if (property.Name == "Id")
                    continue;

                if (property.Name == "SystemActive")
                    parameters.Add("@" + property.Name, true);
                else
                {
                    var value = property.GetValue(entity, null);
                    if (Nullable.GetUnderlyingType(property.PropertyType) != null && value == GetDefault(property.PropertyType))
                        parameters.Add("@" + property.Name, null);
                    else
                        parameters.Add("@" + property.Name, value);
                }
            }

            return parameters;
        }
        protected virtual object UpdateCommandParameters(T entity)
        {
            var parameters = new DynamicParameters();
            var properties = typeof(T).GetProperties();
            foreach (var property in properties)
            {
                if (property.Name == "SystemActive")
                    parameters.Add("@" + property.Name, true);
                else
                {
                    var value = property.GetValue(entity, null);
                    if (Nullable.GetUnderlyingType(property.PropertyType) != null && value == GetDefault(property.PropertyType))
                        parameters.Add("@" + property.Name, null);
                    else
                        parameters.Add("@" + property.Name, value);
                }
            }

            return parameters;

        }
        protected object DeleteCommandParameters(int id)
        {
            return new
            {
                @Id = id
            };
        }
        private object GetDefault(Type type)
        {
            if (type.IsValueType)
                return Activator.CreateInstance(type);

            return null;
        }
    }
}
