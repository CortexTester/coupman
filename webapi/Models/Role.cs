using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace webapi.Models
{
    [JsonConverter(typeof(CustomStringToEnumConverter))]
    public enum Role
    {
        [EnumMember(Value = "Business")]
        Business,
        [EnumMember(Value = "Client")]
        Client
    }

    public class CustomStringToEnumConverter : StringEnumConverter
    {
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (string.IsNullOrEmpty(reader.Value?.ToString()))
            {
                return null;
            }

            object parsedEnumValue;

            var isValidEnumValue = Enum.TryParse(objectType.GenericTypeArguments[0], reader.Value.ToString(), true, out parsedEnumValue);

            if (isValidEnumValue)
            {
                return parsedEnumValue;
            }
            else
            {
                return null;
            }
        }
    }
}