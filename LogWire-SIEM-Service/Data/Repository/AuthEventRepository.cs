using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LogWire.SIEM.Service.Data.Context;
using LogWire.SIEM.Service.Data.Model;
using LogWire.SIEM.Service.Data.Utils.PagedResults;
using LogWire.SIEM.Service.Messages;

namespace LogWire.SIEM.Service.Data.Repository
{
    public class AuthEventRepository : IDataRepository<AuthEventEntry>
    {

        readonly DataContext _context;

        public AuthEventRepository(DataContext context)
        {
            _context = context;
        }

        public IEnumerable<AuthEventEntry> GetAll()
        {
            return _context.AuthEvents.ToList();
        }

        public AuthEventEntry Get(string key)
        {
            return _context.AuthEvents
                .FirstOrDefault(e => e.Id.Equals(key));
        }

        public void Add(AuthEventEntry entity)
        {
            _context.AuthEvents.Add(entity);
            _context.SaveChanges();
        }

        public void Update(AuthEventEntry dbEntity, AuthEventEntry entity)
        {
            dbEntity.EventTime = entity.EventTime;
            dbEntity.EventType = entity.EventType;
            dbEntity.MachineId = entity.MachineId;
            dbEntity.UserId = entity.UserId;

            _context.SaveChanges();
        }

        public void Delete(AuthEventEntry entity)
        {
            _context.AuthEvents.Remove(entity);
            _context.SaveChanges();
        }

        public int Count()
        {
            return _context.Users.Count();
        }

        public PagedResult<AuthEventEntry> GetPagedList(int page, int pageSize)
        {
            return _context.AuthEvents.GetPaged(page, pageSize);
        }

        public bool HasEvent(DateTime eventDate, Guid machineID, Guid userID, string type)
        {
            return _context.AuthEvents.FirstOrDefault(e =>
                       e.EventTime == eventDate && e.MachineId == machineID && e.UserId == userID &&
                       e.EventType == type) != null;
        }
    }
}
