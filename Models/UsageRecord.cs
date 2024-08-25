using System.Text.Json.Serialization;
//+-------------------------------------------------------------------------------------
// 
// Amber Electricity API, (c) Phillip Dimond, 2024
//
// See included licence document.
// 
//+-------------------------------------------------------------------------------------


namespace AmberElectricityAPI.Models;

public class UsageRecord
{
        [JsonPropertyName("type")]
        public string Type { get; set; } = string.Empty;

        [JsonPropertyName("duration")]
        public int Duration { get; set; }

        [JsonPropertyName("date")]
        public DateOnly Date { get; set; }
        
        [JsonPropertyName("endTime")]
        public DateTime EndTime { get; set; }

        [JsonPropertyName("quality")] 
        public string Quality { get; set; } = string.Empty;

        [JsonPropertyName("kwh")]
        public double Kwh { get; set; }
        
        [JsonPropertyName("nemTime")]
        public DateTime NemTime { get; set; }

        [JsonPropertyName("perKwh")]
        public double PerKwh { get; set; }

        [JsonPropertyName("channelType")]
        public ChannelTypeEnum ChannelType { get; set;}

        [JsonPropertyName("channelIdentifier")]
        public string ChannelIdentifier { get; set; } = string.Empty;
        
        [JsonPropertyName("cost")]
        public double Cost { get; set; }
        
        [JsonPropertyName("renewables")]
        public double Renewables { get; set; }
        
        [JsonPropertyName("spotPerKwh")]
        public double SpotPerKwh { get; set; }

        [JsonPropertyName("startTime")] 
        public DateTime StartTime { get; set; }

        [JsonPropertyName("spikeStatus")]
        public SpikeStatusEnum SpikeStatus { get; set; } = SpikeStatusEnum.unknown;

        [JsonPropertyName("tariffInformation")]
        public TariffInfo TariffInformation { get; set; } = new TariffInfo();

        [JsonPropertyName("descriptor")]
        public PriceDescriptorEnum Descriptor { get; set; } = PriceDescriptorEnum.unknown;
}