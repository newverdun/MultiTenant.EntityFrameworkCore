using System;
using System.ComponentModel.DataAnnotations;

namespace WebApiMultiTenantSample.Data.Model
{
    public class Tenant
    {
        [Key]
        public Guid Guid { get; set; }

        public string ConnectionString { get; set; }
        public string Name { get; set; }
    }
}
