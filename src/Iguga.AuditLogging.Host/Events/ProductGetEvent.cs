using Iguga.AuditLogging.Events;
using Iguga.AuditLogging.Host.Dtos;

namespace Iguga.AuditLogging.Host.Events
{
    public class ProductGetEvent : AuditEvent
    {
        public ProductDto Product { get; set; }
    }
}
