using Microsoft.Extensions.DependencyInjection;
using Iguga.AuditLogging.EntityFramework.Extensions;

namespace Iguga.AuditLogging.Extensions
{
    public class AuditLoggingBuilder : IAuditLoggingBuilder
    {
        public AuditLoggingBuilder(IServiceCollection services)
        {
            Services = services;
        }

        public IServiceCollection Services { get; }
    }
}