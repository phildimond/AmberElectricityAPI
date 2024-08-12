using System.Text.Json.Serialization;

namespace AmberElectricityAPI.Models;

[JsonConverter(typeof(JsonStringEnumConverter<PeriodEnum>))]
public enum PeriodEnum
{
    offPeak, 
    shoulder, 
    solarSponge, 
    peak
}