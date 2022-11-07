using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Iguga.AuditLogging.EntityFramework.Entities;

namespace Iguga.AuditLogging.EntityFramework.DbContexts
{
    public interface IAuditLoggingDbContext<TAuditLog> where TAuditLog : AuditLog
    {
        DbSet<TAuditLog> AuditLogs { get; set; }

        Task<int> SaveChangesAsync();
    }
}