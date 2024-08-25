//+-------------------------------------------------------------------------------------
// 
// Amber Electricity API, (c) Phillip Dimond, 2024
//
// See included licence document.
// 
//+-------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace AmberElectricityAPI.Models;

public class Channel
{
    [JsonPropertyName("identifier")] 
    public string Identifier { get; set; } = string.Empty;
    
    [JsonPropertyName("type")]
    public ChannelTypeEnum TypeEnum { get; set; }

    [JsonPropertyName("tariff")] 
    public string Tariff { get; set; } = string.Empty;
}