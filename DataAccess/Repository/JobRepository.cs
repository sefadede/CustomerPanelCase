using DataAccess.Entities;
using DataAccess.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Repository
{
    public class JobRepository : DBContext<Job>, IJobRepository
    {
        public JobRepository(string connectionString)
            : base(connectionString)
        {
        }
        public IEnumerable<Job> GetAll()
        {
            return base.GetAllBase();
        }
        public Job Find(int id)
        {
            return base.FindBase(id);
        }
        public Job Insert(Job entity)
        {
            return base.InsertBase(entity);
        }
        public void Update(Job entity)
        {
            base.UpdateBase(entity);
        }
        public void Delete(int id)
        {
            base.DeleteBase(id);
        }
    }
}
