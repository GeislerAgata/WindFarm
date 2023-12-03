using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace FarmBack.DTO
{
    [BsonIgnoreExtraElements]
    public class Filters
    {
        [JsonProperty("timestamp")]
        public string Timestamp { get; set; }

        [JsonProperty("sensor_id")]
        public string SensorId { get; set; }

        [JsonProperty("sensor_type")]
        public string SensorType { get; set; }
    }
}
