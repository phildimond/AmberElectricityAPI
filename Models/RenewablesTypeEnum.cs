using System.Text.Json.Serialization;

namespace AmberElectricityAPI.Models;

[JsonConverter(typeof(JsonStringEnumConverter<RenewablesTypeEnum>))]
public enum RenewablesTypeEnum
{
    Unknown,
    ActualRenewable,
    CurrentRenewable,
    ForecastRenewable
}