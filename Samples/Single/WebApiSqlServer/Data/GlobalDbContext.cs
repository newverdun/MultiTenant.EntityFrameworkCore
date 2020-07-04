using Microsoft.EntityFrameworkCore;
using WebApiSqlServer.Models;

namespace WebApiSqlServer.Data
{
    public class GlobalDbContext : DbContext
    {
        public DbSet<Tenant> Tenants { get; set; }

        public GlobalDbContext(DbContextOptions<GlobalDbContext> options) : base(options)
        {
        }
    }
}
