using System.Text.Json.Serialization;

namespace AmberElectricityAPI.Models;

[JsonConverter(typeof(JsonStringEnumConverter<PriceDescriptorEnum>))]
public enum PriceDescriptorEnum
{
    negative, 
    extremelyLow, 
    veryLow, 
    low, 
    neutral, 
    high, 
    spike
}