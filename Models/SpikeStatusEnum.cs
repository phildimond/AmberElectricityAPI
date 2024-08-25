//+-------------------------------------------------------------------------------------
// 
// Amber Electricity API, (c) Phillip Dimond, 2024
//
// See included licence document.
// 
//+-------------------------------------------------------------------------------------

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