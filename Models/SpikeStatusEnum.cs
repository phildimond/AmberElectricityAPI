using System.Text.Json.Serialization;

namespace AmberElectricityAPI.Models;

[JsonConverter(typeof(JsonStringEnumConverter<SpikeStatusEnum>))]
public enum SpikeStatusEnum
{
    unknown,
    none, 
    potential, 
    spike
}