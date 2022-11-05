using Iguga.AuditLogging.Parsing;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Iguga.AuditLogging.Helpers.JsonHelpers
{
    /// <summary>
    /// Helper to JSON serialize object data for audit logging.
    /// </summary>
    public static class AuditLogSerializer
    {
        public static readonly JsonSerializerOptions DefaultAuditJsonSettings = new()
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            WriteIndented = true
        };

        public static readonly JsonSerializerOptions BaseAuditEventJsonSettings = new()
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            WriteIndented = true
        };

        static AuditLogSerializer()
        {
            DefaultAuditJsonSettings.Converters.Add(new JsonStringEnumConverter());
            BaseAuditEventJsonSettings.Converters.Add(new JsonStringEnumConverter());
            BaseAuditEventJsonSettings.Converters.Add(new IgnoreAuditEventPropertiesJsonConverter());
        }

        /// <summary>
        /// Serializes the audit event object.
        /// </summary>
        /// <param name="logObject">The object.</param>
        /// <returns></returns>
        public static string Serialize(object logObject)
        {
            return JsonSerializer.Serialize(logObject, DefaultAuditJsonSettings);
        }

        /// <summary>
        /// Serializes the audit event object.
        /// </summary>
        /// <param name="logObject">The object.</param>
        /// <param name="settings"></param>
        /// <returns></returns>
        public static string Serialize(object logObject, JsonSerializerOptions settings)
        {
            return JsonSerializer.Serialize(logObject, settings);
        }
    }
}