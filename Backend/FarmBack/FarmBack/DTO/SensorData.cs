using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace FarmBack.DTO
{
    [BsonIgnoreExtraElements]
    public class SensorData
    {
        [JsonProperty("timestamp")]
        public DateTime Timestamp { get; set; }

        [JsonProperty("sensor_id")]
        public int SensorId { get; set; }

        [JsonProperty("sensor_type")]
        public string SensorType { get; set; }

        [JsonProperty("value")]
        public double Value { get; set; }

        [JsonProperty("unit")]
        public string Unit { get; set; }
    }
    
    public class SensorStats
    {
        [JsonProperty("avg")]
        public double Avg { get; set; }

        [JsonProperty("last_value")]
        public double LastValue { get; set; }
    }
}
