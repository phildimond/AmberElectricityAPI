using System.Text.Json.Serialization;

namespace AmberElectricityAPI.Models;

public class IntervalRecord
{
    [JsonPropertyName("type")] 
    public IntervalTypeEnum IntervalType { get; set; } = IntervalTypeEnum.Unknown;
        
    [JsonPropertyName("date")]
    public DateTime Date { get; set; }
    
    [JsonPropertyName("duration")]
    public int Duration { get; set; }
    
    [JsonPropertyName("startTime")]
    public DateTime StartTime { get; set;}
    
    [JsonPropertyName("endTime")]
    public DateTime EndTime { get; set; }
    
    [JsonPropertyName("nemTime")]
    public DateTime NemTime { get; set; }
    
    [JsonPropertyName("perKwh")]
    public double PerKwh { get; set; }
    
    [JsonPropertyName("renewables")]
    public double Renewables { get; set; }
    
    [JsonPropertyName("spotPerKwh")]
    public double SpotPerKwh { get; set; }
    
    [JsonPropertyName("channelType")]
    public ChannelTypeEnum ChannelType { get; set; } = ChannelTypeEnum.unknown;
    
    [JsonPropertyName("spikeStatus")]
    public SpikeStatusEnum SpikeStatus { get; set; } = SpikeStatusEnum.unknown;

    [JsonPropertyName("tariffInformation")]
    public TariffInfo TariffInformation { get; set; } = new TariffInfo();
    
    [JsonPropertyName("descriptor")]
    public PriceDescriptorEnum Descriptor { get; set; }= PriceDescriptorEnum.unknown;
}