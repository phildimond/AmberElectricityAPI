using System.Text.Json.Serialization;

namespace AmberElectricityAPI.Models;

[JsonConverter(typeof(JsonStringEnumConverter<SiteStatus>))]
public enum SiteStatus
{
    pending, 
    active, 
    closed    
}

public class Site
{
    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("nmi")]
    public string Nmi { get; set; }
    
    [JsonPropertyName("channels")]
    public Channel[] Channels { get; set; } = [];

    [JsonPropertyName("network")]
    public string Network { get; set; } = string.Empty;

    [JsonPropertyName("status")] 
    public SiteStatus Status { get; set; }

    [JsonPropertyName("activeFrom")]
    public DateTime ActiveFrom { get; set; }

    [JsonPropertyName("closedOn")] 
    public DateTime ClosedOn { get; set; }
}