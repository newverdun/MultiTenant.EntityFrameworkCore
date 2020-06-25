using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MultiTenant.EntityFrameworkCore.Configuration.DatabaseTypes;
using MultiTenant.EntityFrameworkCore.Data.Management;
using System;

namespace MultiTenant.EntityFrameworkCore.MultiProviders
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
        /// <typeparam name="TDatabaseType">The class that implements the IDatabaseType interface, dependency injection will be done automatically</typeparam>
        /// <param name="services">The services.</param>
        /// <param name="tenantIdFieldName">HTTP header name to get the tenant ID</param>
        /// <returns>The services.</returns>
        public static IServiceCollection AddMultiTenantEntityFrameworkCore
            <MultiTenantDbContext, 
            TDataBaseManager,
            TDatabaseType>(
            this IServiceCollection services, 
            string tenantIdFieldName)
            where MultiTenantDbContext : DbContext
            where TDataBaseManager : class, IDataBaseManager
            where TDatabaseType : class, IDatabaseType
        {
            services.AddSingleton<IDatabaseType, TDatabaseType>();
            var databaseTypeInstance = services.BuildServiceProvider().GetRequiredService<IDatabaseType>();

            databaseTypeInstance.EnableDatabase(services);

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddDbContext<MultiTenantDbContext>();

            services.AddTransient<IDataBaseManager, TDataBaseManager>();
            services.AddTransient<IContextFactory<MultiTenantDbContext>>(x => new ContextFactory<MultiTenantDbContext>(tenantIdFieldName,x));

            return services;
        }

        /// <summary>
        /// Register dependencies to use MultiTenants
        /// </summary>
        /// <typeparam name="MultiTenantDbContext">DbContext which will be multi tenant</typeparam>
        /// <typeparam name="TDataBaseManager">Implementation to obtain the connections strings corresponding to each tenant</typeparam>
        /// <param name="services">The services.</param>
        /// <param name="tenantIdFieldName">HTTP header name to get the tenant ID</param>
        /// <param name="databaseType">The class that implements the IDatabaseType interface, dependency injection will be done automatically</param>
        /// <returns>The services.</returns>
        public static IServiceCollection AddMultiTenantEntityFrameworkCore
            <MultiTenantDbContext,
            TDataBaseManager>(
            this IServiceCollection services,
            string tenantIdFieldName,
            Type databaseType)
            where MultiTenantDbContext : DbContext
            where TDataBaseManager : class, IDataBaseManager
        {
            services.AddSingleton(typeof(IDatabaseType), databaseType);
            var databaseTypeInstance = services.BuildServiceProvider().GetRequiredService<IDatabaseType>();

            databaseTypeInstance.EnableDatabase(services);

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddDbContext<MultiTenantDbContext>();

            services.AddTransient<IDataBaseManager, TDataBaseManager>();
            services.AddTransient<IContextFactory<MultiTenantDbContext>>(x => new ContextFactory<MultiTenantDbContext>(tenantIdFieldName, x));

            return services;
        }
    }
}
