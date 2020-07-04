using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MultiTenant.EntityFrameworkCore;
using WebApiSqlServer.Data;
using WebApiSqlServer.Models;

namespace WebApiSqlServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly TenantDbContext context;
        public ValuesController(IContextFactory<TenantDbContext> contextFactory)
        {
            context = contextFactory.DbContext;
        }

        [HttpGet]
        public async Task<IEnumerable<TenantConfig>> Get()
        {
            return await context.TenantConfigs.ToListAsync();
        }
    }
}
