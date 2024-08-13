using System.Text.Json.Serialization;

namespace AmberElectricityAPI.Models;

[JsonConverter(typeof(JsonStringEnumConverter<IntervalTypeEnum>))]
public enum IntervalTypeEnum
{
    Unknown,
    ActualInterval,
    CurrentInterval,
    ForecastInterval
}