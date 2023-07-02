using Caso1.Domain.Entities;
using Caso1.Persistence.Contracts.Repositories;
using Caso1.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caso1.Persistence.Repositories
{
    public class UserRepository : Repository<Usuario>, IUserRepository
    {
        public UserRepository(ApplicationDbContext dbContext)
            : base(dbContext)
        {
        }
    }
}
