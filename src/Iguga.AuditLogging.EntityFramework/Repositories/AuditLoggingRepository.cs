using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Iguga.AuditLogging.EntityFramework.DbContexts;
using Iguga.AuditLogging.EntityFramework.Entities;
using Iguga.AuditLogging.EntityFramework.Helpers;
using Iguga.AuditLogging.EntityFramework.Helpers.Common;

namespace Iguga.AuditLogging.EntityFramework.Repositories
{
    public class AuditLoggingRepository<TDbContext, TAuditLog> : IAuditLoggingRepository<TAuditLog>
    where TDbContext : IAuditLoggingDbContext<TAuditLog> 
    where TAuditLog : AuditLog
    {
        protected TDbContext DbContext;

        public AuditLoggingRepository(TDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public virtual async Task<PagedList<TAuditLog>> GetAsync(int page = 1, int pageSize = 10)
        {
            var pagedList = new PagedList<TAuditLog>();

            var auditLogs = await DbContext.AuditLogs
                .PageBy(x => x.Id, page, pageSize)
                .ToListAsync();

            pagedList.Data.AddRange(auditLogs);
            pagedList.PageSize = pageSize;
            pagedList.TotalCount = await DbContext.AuditLogs.CountAsync();


            return pagedList;
        }

        public virtual async Task<PagedList<TAuditLog>> GetAsync(string subjectIdentifier, string subjectName, string category, int page = 1, int pageSize = 10)
        {
            var pagedList = new PagedList<TAuditLog>();

            var auditLogs = await DbContext.AuditLogs
                .WhereIf(!string.IsNullOrWhiteSpace(subjectIdentifier), x => x.SubjectIdentifier == subjectIdentifier)
                .WhereIf(!string.IsNullOrWhiteSpace(subjectName), x => x.SubjectName == subjectName)
                .WhereIf(!string.IsNullOrWhiteSpace(category), x => x.Category == category)
                .PageBy(x => x.Id, page, pageSize)
                .ToListAsync();

            pagedList.Data.AddRange(auditLogs);
            pagedList.PageSize = pageSize;
            pagedList.TotalCount = await DbContext.AuditLogs.CountAsync();


            return pagedList;
        }

        public virtual async Task SaveAsync(TAuditLog auditLog)
        {
            await DbContext.AuditLogs.AddAsync(auditLog);
            await DbContext.SaveChangesAsync();
        }
    }
}