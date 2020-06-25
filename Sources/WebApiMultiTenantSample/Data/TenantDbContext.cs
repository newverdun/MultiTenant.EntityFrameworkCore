using Microsoft.EntityFrameworkCore;
using MultiTenant.EntityFrameworkCore;
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
