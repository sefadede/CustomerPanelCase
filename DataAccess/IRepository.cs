using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess
{
    public interface IRepository<T> where T : EntityBase
    {
        IEnumerable<T> GetAll();
        T Find(int id);
        T Insert(T entity);
        void Update(T entity);
        void Delete(int id);
    }
}
