using Microsoft.EntityFrameworkCore;
using WebApiMultiTenantSample.Data.Model;

namespace WebApiMultiTenantSample.Data
{
    public class TenantDbContext : DbContext
    {
        public TenantDbContext(DbContextOptions<TenantDbContext> options)
            : base(options)
        {

        }
        public DbSet<TenantConfig> TenantConfigs { get; set; }
    }
}
