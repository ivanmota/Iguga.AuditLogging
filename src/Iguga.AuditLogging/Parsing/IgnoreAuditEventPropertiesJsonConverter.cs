using Iguga.AuditLogging.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Iguga.AuditLogging.Parsing
{
    public class IgnoreAuditEventPropertiesJsonConverter : JsonConverterFactory
    {
        public override bool CanConvert(Type typeToConvert)
        {
            if (typeToConvert != typeof(AuditEvent))
            {
                return false;
            }

            return true;
        }

        public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
        {
            return new IgnoreAuditEventPropertiesJsonConverterInner(options);
        }

        private class IgnoreAuditEventPropertiesJsonConverterInner : JsonConverter<AuditEvent>
        {
            private readonly JsonSerializerOptions _options;

            /// <summary>
            /// Ignore base properties of audit event, because these properties are stored separately
            /// </summary>
            private readonly List<string> _propertiesToIgnore = new()
            {
                nameof(AuditEvent.Category),
                nameof(AuditEvent.SubjectAdditionalData),
                nameof(AuditEvent.Action),
                nameof(AuditEvent.SubjectIdentifier),
                nameof(AuditEvent.SubjectType),
                nameof(AuditEvent.SubjectName),
                nameof(AuditEvent.Event),
                nameof(AuditEvent.Source)
            };

            public IgnoreAuditEventPropertiesJsonConverterInner(JsonSerializerOptions options)
            {
                _options = options;
            }

            public override AuditEvent Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                return JsonSerializer.Deserialize<AuditEvent>(ref reader, _options);
            }

            public override void Write(Utf8JsonWriter writer, AuditEvent value, JsonSerializerOptions options)
            {
                if (value is not null)
                {
                    writer.WriteStartObject();

                    foreach (var property in value.GetType().GetProperties().Where(c => !_propertiesToIgnore.Contains(c.Name)))
                    {
                        var propValue = property.GetValue(value);
                        switch (true)
                        {
                            case true when propValue is not null:
                            case true when propValue is null && options.DefaultIgnoreCondition == JsonIgnoreCondition.Never:
                                writer.WritePropertyName(property.Name);
                                JsonSerializer.Serialize(writer, propValue, options);
                                break;
                        }
                    }

                    writer.WriteEndObject();
                }
            }
        }
    }
}
