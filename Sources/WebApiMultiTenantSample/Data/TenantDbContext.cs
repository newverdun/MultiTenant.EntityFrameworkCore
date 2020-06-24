using Microsoft.EntityFrameworkCore;
using MultiTenant.EntityFrameworkCore.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiMultiTenantSample.Data.Model;

namespace WebApiMultiTenantSample.Data
{
    public class TenantDbContext : DbContext, IDbContext
    {
        public TenantDbContext(DbContextOptions<TenantDbContext> options)
            : base(options)
        {

        }
        public DbSet<TenantConfig> TenantConfigs { get; set; }
    }
}
