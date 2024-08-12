using System.Text.Json.Serialization;

namespace AmberElectricityAPI.Models;

public class TariffInfo
{
    [JsonPropertyName("period")]
    public PeriodEnum Period { get; set; }
        
    [JsonPropertyName("season")]
    public string Season { get; set; }
    
    [JsonPropertyName("block")]
    public int Block { get; set; }
    
    [JsonPropertyName("demandWindow")]
    public bool DemandWindow { get; set; }
}