using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace MultiTenant.EntityFrameworkCore
{
    public interface IContextFactory<TDbContext> where TDbContext : DbContext
    {
        TDbContext DbContext { get; }
    }
}
