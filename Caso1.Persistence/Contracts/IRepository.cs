using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Caso1.Persistence.Contracts
{
    public interface IRepository<T>
        where T : class
    {
        T Get(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);

        IEnumerable<T> GetAll(Expression<Func<T, bool>> predicate = null, params Expression<Func<T, object>>[] includes);

        void Insert(T entity);

        void Update(T entity);

        void Delete(T entity);
    }
}
