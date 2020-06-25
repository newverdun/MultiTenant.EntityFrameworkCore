using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace MultiTenant.EntityFrameworkCore.Configuration.DatabaseTypes
{
    /// <summary>
    /// All database connection types should implement this interface to connect via entity framework core.
    /// </summary>
    public interface IDatabaseType
    {
        /// <summary>
        /// Enables database type in the service collection. 
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        IServiceCollection EnableDatabase(IServiceCollection services);

        /// <summary>
        /// updates with new connection string
        /// </summary>
        /// <typeparam name="TContext"></typeparam>
        /// <param name="contextOptionsBuilder"></param>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        DbContextOptionsBuilder<TContext> SetConnectionString<TContext>(DbContextOptionsBuilder<TContext> contextOptionsBuilder, string connectionString) where TContext : DbContext;
    }
}
