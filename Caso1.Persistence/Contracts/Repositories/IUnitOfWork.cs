﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caso1.Persistence.Contracts.Repositories
{
    public interface IUnitOfWork<out T>
        where T : DbContext
    {
        T ContexT { get; }

        void BeginTransaction();
        void CommitTransaction();
        void RollbackTransaction();

        void Save();

        IRepository<E> Repository<E>()
            where E : class;

    }
}
