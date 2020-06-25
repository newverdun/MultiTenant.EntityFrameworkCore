using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MultiTenant.EntityFrameworkCore.Configuration.DatabaseTypes;

namespace MultiTenant.EntityFrameworkCore.PostgreSQL.Configuration.DatabaseTypes
{
    /// <inherit/>
    public class Postgres : IDatabaseType
    {
        /// <inherit/>
        public IServiceCollection EnableDatabase(IServiceCollection services)
        {
            services.AddEntityFrameworkNpgsql();
            return services;
        }

        /// <inherit/>
        public DbContextOptionsBuilder<TContext> SetConnectionString<TContext>(DbContextOptionsBuilder<TContext> contextOptionsBuilder, string connectionString) where TContext : DbContext
        {
            return contextOptionsBuilder.UseNpgsql(connectionString);
        }
    }
}
