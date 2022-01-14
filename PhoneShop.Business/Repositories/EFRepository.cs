using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Phoneshop.Business.Interfaces;

namespace PhoneShop.Business.Repositories
{
    [ExcludeFromCodeCoverage]
    public class EFRepository<T> : IRepository<T> where T : class
    {
        private readonly DataContext dataContext;

        public EFRepository(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }
        public void Create(T entity)
        {
            dataContext.Add(entity);
        }

        public void Delete(T entity)
        {
            dataContext.Set<T>().Remove(entity);
        }

        public IEnumerable<T> Get()
        {
            return dataContext.Set<T>().ToList();
        }

        public T Get(int id)
        {
            return dataContext.Set<T>().Find(id);
        }

        public IQueryable<T> GetWithRelatedData(int id, params Expression<Func<T, object>>[] includes)
        {
            var query = dataContext.Set<T>();
            IQueryable<T> queryable = query.AsQueryable();

            foreach (var include in includes)
            {
                queryable = query.Include(include);
            }

            return queryable;
        }

        public IEnumerable<T> GetWithRelatedData(params Expression<Func<T, object>>[] includes)
        {
            var query = dataContext.Set<T>();
            IQueryable<T> queryable = query.AsQueryable();

            foreach (var include in includes)
            {
                queryable = query.Include(include);
            }

            return queryable.ToList();
        }

        public void Save()
        {
            dataContext.SaveChanges();
        }
    }
}
