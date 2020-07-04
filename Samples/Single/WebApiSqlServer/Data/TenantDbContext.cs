using Microsoft.EntityFrameworkCore;
using WebApiSqlServer.Models;

namespace WebApiSqlServer.Data
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
