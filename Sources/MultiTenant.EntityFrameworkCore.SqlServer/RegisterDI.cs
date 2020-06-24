using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MultiTenant.EntityFrameworkCore.Configuration.DatabaseTypes;
using MultiTenant.EntityFrameworkCore.Data;
using MultiTenant.EntityFrameworkCore.Data.Management;
using MultiTenant.EntityFrameworkCore.SqlServer.Configuration.DatabaseTypes;

namespace MultiTenant.EntityFrameworkCore.SqlServer
{
    /// <summary>
    /// Register dependencies
    /// </summary>
    public static class RegisterDI
    {
        /// <summary>
        /// Register dependencies to use MultiTenants
        /// </summary>
        /// <typeparam name="MultiTenantDbContext">DbContext which will be multi tenant</typeparam>
        /// <typeparam name="TDataBaseManager">Implementation to obtain the connections strings corresponding to each tenant</typeparam>
        /// <param name="services">The services.</param>
        /// <param name="tenantIdFieldName">HTTP header name to get the tenant ID</param>
        /// <returns>The services.</returns>
        public static IServiceCollection AddMultiTenantEntityFrameworkCore<MultiTenantDbContext, TDataBaseManager>(this IServiceCollection services, string tenantIdFieldName)
            where MultiTenantDbContext : DbContext, IDbContext
            where TDataBaseManager : class, IDataBaseManager
        {
            services.AddSingleton<IDatabaseType, MsSql>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddDbContext<MultiTenantDbContext>();

            services.AddScoped<IDbContext, MultiTenantDbContext>();

            services.AddTransient<IDataBaseManager, TDataBaseManager>();
            services.AddTransient<IContextFactory>(x => new ContextFactory<MultiTenantDbContext>(tenantIdFieldName, x));

            return services;
        }
    }
}
