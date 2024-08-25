//+-------------------------------------------------------------------------------------
// 
// Amber Electricity API, (c) Phillip Dimond, 2024
//
// See included licence document.
// 
//+-------------------------------------------------------------------------------------

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