using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using LogWire.SIEM.Service.Data.Context;
using LogWire.SIEM.Service.Data.Model;
using LogWire.SIEM.Service.Data.Utils.PagedResults;

namespace LogWire.SIEM.Service.Data.Repository
{
    public class MachineRepository : IDataRepository<MachineEntry>
    {
        readonly DataContext _context;

        public MachineRepository(DataContext context)
        {
            _context = context;
        }

        public IEnumerable<MachineEntry> GetAll()
        {
            return _context.Machines.ToList();
        }

        public MachineEntry Get(string key)
        {
            return _context.Machines
                .FirstOrDefault(e => e.Name.Equals(key));
        }

        public void Add(MachineEntry entity)
        {
            _context.Machines.Add(entity);
            _context.SaveChanges();
        }

        public void Update(MachineEntry dbEntity, MachineEntry entity)
        {
            dbEntity.Name = entity.Name;
            dbEntity.Ip = entity.Ip;

            _context.SaveChanges();
        }

        public void Delete(MachineEntry entity)
        {
            _context.Machines.Remove(entity);
            _context.SaveChanges();
        }

        public int Count()
        {
            return _context.Users.Count();
        }

        public PagedResult<MachineEntry> GetPagedList(int page, int pageSize)
        {
            return _context.Machines.GetPaged(page, pageSize);
        }
    }
}
