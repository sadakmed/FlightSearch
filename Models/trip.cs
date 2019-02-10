using System;
using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace travel.Models
{
   

    public partial class trip
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("departure")]
        public Departure Departure { get; set; }

        [JsonProperty("arrival")]
        public Arrival Arrival { get; set; }

        [JsonProperty("airline")]
        public Airline Airline { get; set; }

        [JsonProperty("flight")]
        public Flight Flight { get; set; }
    }

    public partial class Airline
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("iataCode")]
        public string IataCode { get; set; }

        [JsonProperty("icaoCode")]
        public string IcaoCode { get; set; }
    }

    public partial class Arrival
    {
        [JsonProperty("iataCode")]
        public string IataCode { get; set; }

        [JsonProperty("icaoCode")]
        public string IcaoCode { get; set; }

        [JsonProperty("terminal")]
        public string Terminal { get; set; }

        [JsonProperty("delay")]
        public long Delay { get; set; }

        [JsonProperty("scheduledTime")]
        public DateTimeOffset ScheduledTime { get; set; }

        [JsonProperty("estimatedTime")]
        public DateTimeOffset EstimatedTime { get; set; }

        [JsonProperty("estimatedRunway")]
        public DateTimeOffset EstimatedRunway { get; set; }

        [JsonProperty("actualRunway")]
        public DateTimeOffset ActualRunway { get; set; }
    }

    public partial class Departure
    {
        [JsonProperty("iataCode")]
        public string IataCode { get; set; }

        [JsonProperty("icaoCode")]
        public string IcaoCode { get; set; }

        [JsonProperty("scheduledTime")]
        public DateTimeOffset ScheduledTime { get; set; }

        [JsonProperty("estimatedRunway")]
        public DateTimeOffset EstimatedRunway { get; set; }

        [JsonProperty("actualRunway")]
        public DateTimeOffset ActualRunway { get; set; }
    }

    public partial class Flight
    {
        [JsonProperty("number")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Number { get; set; }

        [JsonProperty("iataNumber")]
        public string IataNumber { get; set; }

        [JsonProperty("icaoNumber")]
        public string IcaoNumber { get; set; }
    }

    public partial class trip
    {
        public static trip FromJson(string json) => JsonConvert.DeserializeObject<trip>(json, travel.Models.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this trip self) => JsonConvert.SerializeObject(self, travel.Models.Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    internal class ParseStringConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(long) || t == typeof(long?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            long l;
            if (Int64.TryParse(value, out l))
            {
                return l;
            }
            throw new Exception("Cannot unmarshal type long");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (long)untypedValue;
            serializer.Serialize(writer, value.ToString());
            return;
        }

        public static readonly ParseStringConverter Singleton = new ParseStringConverter();
    }
}