using Microsoft.Extensions.DependencyInjection;

namespace Iguga.AuditLogging.Extensions
{
    public interface IAuditLoggingBuilder
    {
        IServiceCollection Services { get; }
    }
}