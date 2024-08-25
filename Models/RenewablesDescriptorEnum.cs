//+-------------------------------------------------------------------------------------
// 
// Amber Electricity API, (c) Phillip Dimond, 2024
//
// See included licence document.
// 
//+-------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace AmberElectricityAPI.Models;

// Renewables Descriptors. Amber don't document this. Extracted from responses 

[JsonConverter(typeof(JsonStringEnumConverter<RenewablesDescriptorEnum>))]
public enum RenewablesDescriptorEnum
{
    unknown,
    worst,
    notGreat,
    ok,
    great,
    best
}