using System;
using System.Collections.Generic;

namespace InTechStore.DAL.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> GetAllWithTracking();
        IEnumerable<TEntity> Get(Func<TEntity, bool> predicate);
        TEntity FindById(int id);
        IEnumerable<TEntity> Find(Func<TEntity, bool> predicate);
        void Create(TEntity item);
        void Update(TEntity item);
        void Remove(TEntity item);
    }
}
