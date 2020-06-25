using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace MultiTenant.EntityFrameworkCore
{
    /// <summary>
    /// Context factory interface
    /// </summary>
    /// <typeparam name="TDbContext"></typeparam>
    public interface IContextFactory<TDbContext> where TDbContext : DbContext
    {
        /// <summary>
        /// Gets the database context.
        /// </summary>
        /// <value>
        /// The database context.
        /// </value>
        TDbContext DbContext { get; }
    }
}
