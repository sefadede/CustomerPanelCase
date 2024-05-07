using Dapper;
using DataAccess.Entities.Custom;
using DataAccess.Entities;
using DataAccess.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class ChatRepository : DBContext<Chat>, IChatRepository
    {
        public ChatRepository(string connectionString)
            : base(connectionString)
        {
        }
        public IEnumerable<Chat> GetAll()
        {
            return base.GetAllBase();
        }
        public Chat Find(int id)
        {
            return base.FindBase(id);
        }
        public Chat Insert(Chat entity)
        {
            return base.InsertBase(entity);
        }
        public void Update(Chat entity)
        {
            base.UpdateBase(entity);
        }
        public void Delete(int id)
        {
            base.DeleteBase(id);
        }
    }
}
