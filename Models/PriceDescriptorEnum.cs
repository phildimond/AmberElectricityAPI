using System.Text.Json.Serialization;

namespace AmberElectricityAPI.Models;

[JsonConverter(typeof(JsonStringEnumConverter<PriceDescriptorEnum>))]
public enum PriceDescriptorEnum
{
    unknown,
    negative, 
    extremelyLow, 
    veryLow, 
    low, 
    neutral, 
    high, 
    spike
}