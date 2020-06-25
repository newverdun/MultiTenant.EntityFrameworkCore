namespace MultiTenant.EntityFrameworkCore
{
    /// <summary>
    /// Multi tenant database manager
    /// </summary>
    public interface IDataBaseManager
    {
        /// <summary>
        /// Gets the connection string of the data base.
        /// </summary>
        /// <param name="tenantId">The tenant identifier.</param>
        /// <returns>Connection String</returns>
        string GetConnectionString(string tenantId);
    }
}
