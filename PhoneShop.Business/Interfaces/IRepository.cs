using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Phoneshop.Business.Interfaces
{
    public interface IRepository<T> where T : class
    {
        T Get(int id);
        IEnumerable<T> Get();
        void Delete(T entity);
        void Create(T entity);
        void Save();
        IQueryable<T> GetWithRelatedData(int id, params Expression<Func<T, object>>[] includes);
        IEnumerable<T> GetWithRelatedData(params Expression<Func<T, object>>[] includes);
    }
}
