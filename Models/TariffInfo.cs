//+-------------------------------------------------------------------------------------
// 
// Amber Electricity API, (c) Phillip Dimond, 2024
//
// See included licence document.
// 
//+-------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace AmberElectricityAPI.Models;

public class TariffInfo
{
    [JsonPropertyName("period")]
    public PeriodEnum Period { get; set; }
        
    [JsonPropertyName("season")]
    public string Season { get; set; } = string.Empty;
    
    [JsonPropertyName("block")]
    public int Block { get; set; }
    
    [JsonPropertyName("demandWindow")]
    public bool DemandWindow { get; set; }
}