using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MultiTenant.EntityFrameworkCore.Configuration.DatabaseTypes;
using System;
using System.Collections.Generic;
using System.Text;

namespace MultiTenant.EntityFrameworkCore.Data.Management
{
    /// <summary>
    /// Entity Framework context service
    /// (Switches the db context according to tenant id field)
    /// </summary>
    /// <typeparam name="TDbContext">DbContext to whom the connection change will be made, according to the TenantId</typeparam>
    /// <seealso cref="IContextFactory{TDbContext}"/>
    public class ContextFactory<TDbContext> : IContextFactory<TDbContext> where TDbContext : DbContext
    {
        private readonly string TenantIdFieldName;
        private readonly HttpContext httpContext;

        private readonly IDataBaseManager dataBaseManager;
        private readonly IDatabaseType databaseType;

        /// <summary>
        /// Initialize the ContextFactory
        /// </summary>
        /// <param name="tenantIdFieldName">The HTTP header name to get the tenant ID</param>
        /// <param name="serviceProvider">The service provider</param>
        public ContextFactory(
            string tenantIdFieldName,
            IServiceProvider serviceProvider)
        {
            TenantIdFieldName = tenantIdFieldName;
            httpContext = serviceProvider.GetRequiredService<IHttpContextAccessor>().HttpContext;
            dataBaseManager = serviceProvider.GetRequiredService<IDataBaseManager>();
            databaseType = serviceProvider.GetRequiredService<IDatabaseType>();
        }

        /// <inheritdoc />
        public TDbContext DbContext 
        {
            get
            {
                var options = ChangeConnectionString(this.TenantId).Options;
                TDbContext dbContext = (TDbContext)Activator.CreateInstance(typeof(TDbContext), options);

                return dbContext;
            }
        }

        /// <inheritdoc />
        public TDbContext GetDbContext(string tenantId)
        {
            var options = ChangeConnectionString(tenantId).Options;
            TDbContext dbContext = (TDbContext)Activator.CreateInstance(typeof(TDbContext), options);

            return dbContext;
        }

        /// <summary>
        /// Gets tenant id from HTTP header
        /// </summary>
        /// <value>
        /// The tenant identifier.
        /// </value>
        /// <exception cref="ArgumentNullException">
        /// httpContext
        /// or
        /// tenantId
        /// </exception>
        private string TenantId
        {
            get
            {
                ValidateHttpContext();

                string tenantId = this.httpContext.Request.Headers[TenantIdFieldName].ToString();

                ValidateTenantId(tenantId);

                return tenantId;
            }
        }

        private void ValidateHttpContext()
        {
            if (this.httpContext == null)
            {
                throw new ArgumentNullException(nameof(this.httpContext));
            }
        }

        private static void ValidateTenantId(string tenantId)
        {
            if (string.IsNullOrEmpty(tenantId))
            {
                throw new ArgumentNullException(nameof(tenantId));
            }
        }

        private DbContextOptionsBuilder<TDbContext> ChangeConnectionString(string tenantId) //where TDbContext : DbContext
        {
            // 1. Obtain Connection String from DataBaseManager and Add new DB name to 
            var connectionString = dataBaseManager.GetConnectionString(tenantId);

            // 2. Create DbContextOptionsBuilder with new Database name
            var contextOptionsBuilder = new DbContextOptionsBuilder<TDbContext>();

            databaseType.SetConnectionString(contextOptionsBuilder, connectionString);

            return contextOptionsBuilder;
        }
    }
}
