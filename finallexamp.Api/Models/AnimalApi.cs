using System.Text.Json.Serialization;

namespace finallexamp.Api.Models
{
    public class AnimalApi
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("scientific_name")]
        public string? ScientificName { get; set; }
        [JsonPropertyName("conservation_status")]
        public string? ConservationStatus { get; set; }
        [JsonPropertyName("group")]
        public string? GroupName { get; set; }

        [JsonPropertyName("iso_code")]
        public string? CountryCode { get; set; }
        [JsonPropertyName("common_name")]
        public string? Name { get; set; }
        
        public override string ToString()
        {
            return $"{Id}" +
                   $"\n{Name}" +
                   $"\n{ScientificName}" +
                   $"\n{GroupName}" +
                   $"\n{ConservationStatus}" +
                   $"\n{CountryCode}";
        }
    }
}


