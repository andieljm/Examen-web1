using Caso1.Persistence.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Caso1.Persistence.DbContexts
{
    public class Repository<T> : IRepository<T>
        where T : class
    {

        private readonly DbContext _dbcontext;
        private readonly DbSet<T> _dbSet;

        public Repository(DbContext dbContext)
        {
            _dbcontext = dbContext;
            _dbSet = _dbcontext.Set<T>();
        }


        public T Get(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet;
            query = query.Where(predicate);


            return query.FirstOrDefault();
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> predicate = null, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet;

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (includes.Any())
            {
                query = includes.Aggregate
                    (query, (current, include) => current.Include(include));
            }
            return query;
        }

        public void Insert(T entity)
        {
            if (entity == null)
            {
                throw new NotImplementedException();
            }
            _dbSet.Add(entity);
        }

        public void Update(T entity)
        {
            if (entity == null)
            {
                throw new NotImplementedException();
            }
            _dbSet.Update(entity);
        }

        public void Delete(T entity)
        {
            if (entity == null)
            {
                throw new NotImplementedException();
            }
            _dbSet.Remove(entity);
        }
    }
}
