using MultiTenant.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiSqlServer.Data
{
    public class DataBaseManager : IDataBaseManager
    {
        private readonly GlobalDbContext context;
        public DataBaseManager(GlobalDbContext context)
        {
            this.context = context;
        }

        /*
          IMPORTANT NOTICE: Tenant Configuration was implemented for demo purposes only 
          In a production application I would recommend following options:
            - create SQL root database or table (Current)
            - create NoSql root database/collection
            - move the configuration the Redis cache  
         */
        public string GetConnectionString(string tenantId)
        {
            var tenant = context.Tenants.FirstOrDefault(x => x.Guid == Guid.Parse(tenantId));
            if (tenant != null)
            {
                return tenant.ConnectionString;
            }
            return string.Empty;
        }
    }
}
