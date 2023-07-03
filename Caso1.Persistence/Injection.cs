using Caso1.Domain.Entities;
using Caso1.Persistence.Contracts;
using Caso1.Persistence.Contracts.Repositories;
using Caso1.Persistence.DbContexts;
using Caso1.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caso1.Persistence
{
    public static class Injection
    {
        public static IServiceCollection AddPersistenceServices
            (this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>
                (options => options.UseSqlServer(configuration.GetConnectionString("Default")));

            services.AddScoped<IApplicationDbContext>
                (options => options.GetService<ApplicationDbContext>());

            services.AddUnitOfWork<ApplicationDbContext>()
                .AddRepository<Usuario, UserRepository>();

            services.AddUnitOfWork<ApplicationDbContext>()
                .AddRepository<Tarea, TareasRepository>();




            return services;
        }
    }
}
