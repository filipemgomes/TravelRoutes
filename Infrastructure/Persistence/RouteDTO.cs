using System.Text.Json.Serialization;

namespace InfraStructure.Persistence
{
    public class RouteDTO
    {
        [JsonPropertyName("origin")]
        public string Origin { get; set; }

        [JsonPropertyName("destination")]
        public string Destination { get; set; }

        [JsonPropertyName("cost")]
        public decimal Cost { get; set; }
    }
}
