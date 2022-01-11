using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Phoneshop.Business.Interfaces;

namespace PhoneShop.Business.Repositories
{
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

        public T GetWithRelatedData(int id, params Expression<Func<T, object>>[] includes)
        {
            var query = dataContext.Set<T>();

            foreach (var include in includes)
            {
                query.Include(include);
            }

            return query.Find(id);
        }

        public IEnumerable<T> GetWithRelatedData(params Expression<Func<T, object>>[] includes)
        {
            var query = dataContext.Set<T>();

            foreach (var include in includes)
            {
                query.Include(include);
            }

            return query.ToList();
        }

        public void Save()
        {
            dataContext.SaveChanges();
        }
    }
}
