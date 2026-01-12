using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Project.Persistance.ContextClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Persistance.DependencyResolvers
{
    public static class DbContextInjection
    {
        public static void AddDbContextInjection(this IServiceCollection services, IConfiguration configuration)
        {


            services.AddDbContext<MyContext>(x => x.UseSqlServer(configuration.GetConnectionString("OnionDb")).UseLazyLoadingProxies());

        }
    }
}
