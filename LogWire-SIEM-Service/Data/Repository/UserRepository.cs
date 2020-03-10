using System.Collections.Generic;
using System.Linq;
using LogWire.SIEM.Service.Data.Context;
using LogWire.SIEM.Service.Data.Model;
using LogWire.SIEM.Service.Data.Utils.PagedResults;

namespace LogWire.SIEM.Service.Data.Repository
{
    public class UserRepository : IDataRepository<UserEntry>
    {
        readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }

        public IEnumerable<UserEntry> GetAll()
        {
            return _context.Users.ToList();
        }

        public UserEntry Get(string key)
        {
            return _context.Users
                .FirstOrDefault(e => e.Username.Equals(key));
        }

        public void Add(UserEntry entity)
        {
            _context.Users.Add(entity);
            _context.SaveChanges();
        }

        public void Update(UserEntry dbEntity, UserEntry entity)
        {
            dbEntity.Username = entity.Username;

            _context.SaveChanges();
        }

        public void Delete(UserEntry entity)
        {
            _context.Users.Remove(entity);
            _context.SaveChanges();
        }

        public int Count()
        {
            return _context.Users.Count();
        }

        public PagedResult<UserEntry> GetPagedList(int page, int pageSize)
        {
            return _context.Users.GetPaged(page, pageSize);
        }

    }
}
