using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Iguga.AuditLogging.EntityFramework.Entities;

namespace Iguga.AuditLogging.EntityFramework.DbContexts
{
    public abstract class AuditLoggingDbContext<TAuditLog> : DbContext, IAuditLoggingDbContext<TAuditLog> 
        where TAuditLog : AuditLog

    {
        protected AuditLoggingDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<TAuditLog> AuditLogs { get; set; }

        public Task<int> SaveChangesAsync()
        {
            return base.SaveChangesAsync();
        }
    }
}
