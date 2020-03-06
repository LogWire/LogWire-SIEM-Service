using System.Collections.Generic;

namespace LogWire.SIEM.Service.Data.Repository
{
    public interface IDataRepository<TEntity>
    {

        IEnumerable<TEntity> GetAll();
        TEntity Get(string key);
        void Add(TEntity entity);
        void Update(TEntity dbEntity, TEntity entity);
        void Delete(TEntity entity);
        int Count();
    }
}
