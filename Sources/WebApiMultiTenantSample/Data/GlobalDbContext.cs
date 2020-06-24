using Microsoft.EntityFrameworkCore;
using WebApiMultiTenantSample.Data.Model;

namespace WebApiMultiTenantSample.Data
{
    public class GlobalDbContext : DbContext
    {
        public DbSet<Tenant> Tenants { get; set; }

        public GlobalDbContext(DbContextOptions<GlobalDbContext> options) : base(options)
        {
        }
    }
}
