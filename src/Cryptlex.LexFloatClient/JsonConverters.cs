#if NETSTANDARD2_0
using System;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Cryptlex
{
    internal sealed class LongToStringConverter : JsonConverter<string>
    {
        public override string Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.Number)
            {
                if (reader.TryGetInt64(out long longValue))
                {
                    return longValue.ToString(CultureInfo.InvariantCulture);
                }
            }

            throw new JsonException("Expected string or number.");
        }

        public override void Write(Utf8JsonWriter writer, string value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value);
        }
    }
}
#endif // NETSTANDARD2_0
