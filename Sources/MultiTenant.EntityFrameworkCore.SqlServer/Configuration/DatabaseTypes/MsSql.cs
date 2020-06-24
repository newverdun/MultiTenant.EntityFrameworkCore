using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MultiTenant.EntityFrameworkCore.Configuration.DatabaseTypes;

namespace MultiTenant.EntityFrameworkCore.SqlServer.Configuration.DatabaseTypes
{
    public class MsSql : IDatabaseType
    {
        public IServiceCollection EnableDatabase(IServiceCollection services)
        {
            return services;
        }

        public DbContextOptionsBuilder<TContext> SetConnectionString<TContext>(DbContextOptionsBuilder<TContext> contextOptionsBuilder, string connectionString) where TContext : DbContext
        {
            return contextOptionsBuilder.UseSqlServer(connectionString);
        }
    }
}
