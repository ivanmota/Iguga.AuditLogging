using System;
using System.Threading.Tasks;
using Iguga.AuditLogging.EntityFramework.Entities;
using Iguga.AuditLogging.EntityFramework.Mapping;
using Iguga.AuditLogging.EntityFramework.Repositories;
using Iguga.AuditLogging.Events;
using Iguga.AuditLogging.Services;

namespace Iguga.AuditLogging.EntityFramework.Services
{
    public class DatabaseAuditEventLoggerSink<TAuditLog> : IAuditEventLoggerSink 
        where TAuditLog : AuditLog, new()
    {
        private readonly IAuditLoggingRepository<TAuditLog> _auditLoggingRepository;

        public DatabaseAuditEventLoggerSink(IAuditLoggingRepository<TAuditLog> auditLoggingRepository)
        {
            _auditLoggingRepository = auditLoggingRepository;
        }

        public virtual async Task PersistAsync(AuditEvent auditEvent)
        {
            if (auditEvent == null) throw new ArgumentNullException(nameof(auditEvent));

            var auditLog = auditEvent.MapToEntity<TAuditLog>();

            await _auditLoggingRepository.SaveAsync(auditLog);
        }
    }
}