using System.Text.Json.Serialization;

namespace AmberElectricityAPI.Models;

public class RenewablesRecord
{
    [JsonPropertyName("type")] 
    public RenewablesTypeEnum Type {get; set; }= RenewablesTypeEnum.Unknown;
    
    [JsonPropertyName("duration")] 
    public int Duration { get; set; }
    
    [JsonPropertyName("date")] 
    public DateTime Date { get; set; }
    
    [JsonPropertyName("startTime")] 
    public DateTime StartTime { get; set; }
    
    [JsonPropertyName("endTime")] 
    public DateTime EndTime { get; set; }
    
    [JsonPropertyName("nemTime")] 
    public DateTime NemTime { get; set; }

    [JsonPropertyName("renewables")] 
    public double Renewables { get; set; }

    [JsonPropertyName("descriptor")]
    public RenewablesDescriptorEnum Descriptor { get; set; } = RenewablesDescriptorEnum.unknown;
}