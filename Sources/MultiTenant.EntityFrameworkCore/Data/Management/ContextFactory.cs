using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MultiTenant.EntityFrameworkCore.Configuration.DatabaseTypes;
using System;

namespace MultiTenant.EntityFrameworkCore.Data.Management
{
    public class ContextFactory<MultiTenantDbContext> : IContextFactory where MultiTenantDbContext : DbContext, IDbContext
    {
        private readonly string TenantIdFieldName;
        private readonly HttpContext httpContext;

        private readonly IDataBaseManager dataBaseManager;
        private readonly IDatabaseType databaseType;
        public ContextFactory(
            string tenantIdFieldName,
            IServiceProvider serviceProvider)
        {
            TenantIdFieldName = tenantIdFieldName;
            httpContext = serviceProvider.GetRequiredService<IHttpContextAccessor>().HttpContext;
            dataBaseManager = serviceProvider.GetRequiredService<IDataBaseManager>();
            databaseType = serviceProvider.GetRequiredService<IDatabaseType>();
        }
        public IDbContext DbContext
        {
            get
            {
                var options = ChangeConnectionString(this.TenantId).Options;
                MultiTenantDbContext dbContext = (MultiTenantDbContext)Activator.CreateInstance(typeof(MultiTenantDbContext), options);
                
                return dbContext;
            }
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

        private DbContextOptionsBuilder<MultiTenantDbContext> ChangeConnectionString(string tenantId) //where TDbContext : DbContext
        {
            // 1. Obtain Connection String from DataBaseManager and Add new DB name to 
            var connectionString = dataBaseManager.GetConnectionString(tenantId);

            // 2. Create DbContextOptionsBuilder with new Database name
            var contextOptionsBuilder = new DbContextOptionsBuilder<MultiTenantDbContext>();

            databaseType.SetConnectionString(contextOptionsBuilder, connectionString);

            return contextOptionsBuilder;
        }
    }
}
