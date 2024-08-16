using System.Text.Json.Serialization;

namespace AmberElectricityAPI.Models;

[JsonConverter(typeof(JsonStringEnumConverter<RenewablesStateEnum>))]
public enum RenewablesStateEnum
{
    Unknown,
    NewSouthWales,
    Victoria,
    SouthAustralia,
    Queensland
}