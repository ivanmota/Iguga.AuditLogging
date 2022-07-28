using Iguga.AuditLogging.Events;
using Iguga.AuditLogging.Host.Dtos;

namespace Iguga.AuditLogging.Host.Events
{
    public class GenericProductEvent<T1, T2, T3> : AuditEvent
    {
        public ProductDto Product { get; set; }
    }
}
