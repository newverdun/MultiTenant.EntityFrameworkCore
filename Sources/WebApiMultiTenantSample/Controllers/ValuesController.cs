using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MultiTenant.EntityFrameworkCore.Data.Management;
using WebApiMultiTenantSample.Data;
using WebApiMultiTenantSample.Data.Model;

namespace WebApiMultiTenantSample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly TenantDbContext tenantDbContext;
        public ValuesController(IContextFactory factory)
        {
            tenantDbContext = (TenantDbContext)factory.DbContext;
        }

        // GET: api/<ValuesController>
        [HttpGet]
        public string Get()
        {
            tenantDbContext.Database.EnsureCreated();

            if (!tenantDbContext.TenantConfigs.Any())
            {
                tenantDbContext.TenantConfigs.Add(new TenantConfig() { Config = $"This is the config for {tenantDbContext.Database.GetDbConnection().Database}. We are using the ConnectionString {tenantDbContext.Database.GetDbConnection().ConnectionString}" });
                tenantDbContext.SaveChanges();
            }

            return tenantDbContext.TenantConfigs.FirstOrDefault()?.Config;
        }
    }
}
