using System.Text.Json.Serialization;

namespace AmberElectricityAPI.Models;

[JsonConverter(typeof(JsonStringEnumConverter<ChannelTypeEnum>))]
public enum ChannelTypeEnum
{
    unknown,
    general,
    controlledLoad,
    feedIn
}
