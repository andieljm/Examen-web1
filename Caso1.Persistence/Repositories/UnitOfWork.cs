using Caso1.Persistence.Contracts;
using Caso1.Persistence.Contracts.Repositories;
using Caso1.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caso1.Persistence.Repositories
{
    internal class UnitOfWork<T> : IUnitOfWork<T>, IDisposable
        where T : DbContext
    {

        private readonly T _dbcontext;
        private readonly IDictionary<string, object> _repositories;

        private IDbContextTransaction _transaction;

        private bool _isDisposed = false;
        public UnitOfWork(T dbcontext)
        {
            _dbcontext = dbcontext;
            _repositories = new Dictionary<string, object>();
        }
        public T ContexT
        {
            get { return _dbcontext; }
        }

        public void BeginTransaction()
        {
            if (_transaction != null)
            {
                throw new NotImplementedException();
            }

            _transaction = _dbcontext.Database.BeginTransaction();
        }

        public void CommitTransaction()
        {
            if (_transaction == null)
            {
                throw new NotImplementedException();
            }

            _transaction.Commit();
            _transaction.Dispose();
            _transaction = null;
        }

        public void RollbackTransaction()
        {
            if (_transaction == null)
            {
                throw new NotImplementedException();
            }

            _transaction.Rollback();
            _transaction.Dispose();
            _transaction = null;
        }

        public void Save()
        {
            _dbcontext.SaveChanges();
        }
        public IRepository<E> Repository<E>() where E : class
        {
            var repository = _dbcontext.GetService<IRepository<E>>();

            if (repository != null)
            {
                return repository;
            }

            var type = typeof(E);
            var typeName = type.Name;

            if (!_repositories.ContainsKey(typeName))
            {
                var instance = new Repository<E>(_dbcontext);
                _repositories.Add(typeName, instance);
            }

            return (IRepository<E>)_repositories[typeName];
        }

        public void Dispose()
        {
            Dispose(true);
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (!_isDisposed)
                {
                    if (_transaction != null)
                    {
                        _transaction.Dispose();
                    }

                    _dbcontext.Dispose();
                }
            }
            _isDisposed = true;
        }
    }
}
