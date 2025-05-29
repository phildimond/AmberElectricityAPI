namespace AmberElectricityAPI.Models;

/// <summary>
/// Current API calls information from most recent API call
/// </summary>public class ApiRateInformation
public class ApiRateInformation
{
    /// <summary>
    /// How many API calls remain in this time window
    /// </summary>
    public int ApiCallsRemaining { get; set; }
    
    /// <summary>
    /// How many seconds remain in this time window
    /// </summary>
    public int WindowSecsRemaining { get; set; }
}