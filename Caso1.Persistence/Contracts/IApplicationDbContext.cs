using Caso1.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caso1.Persistence.Contracts
{
    public interface IApplicationDbContext
    {
        DbSet<Tarea> Tareas { get; set; }

        DbSet<Usuario> Usuarios { get; set; }

        void save();
    }
}
