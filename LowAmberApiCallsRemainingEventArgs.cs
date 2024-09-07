namespace EnergySuper.EventArgsAndHandlers;

public delegate void LowAmberApiCallsRemainingEventHandler(object? sender, LowAmberApiCallsRemainingEventArgs e); 

/// <summary>
/// Arguments for a low API calls remaining event
/// </summary>
/// <param name="apiCallsRemaining">How many API calls remain in this time window</param>
/// <param name="windowSecsRemaining">How many seconds remain in this time window</param>
public class LowAmberApiCallsRemainingEventArgs(int apiCallsRemaining, int windowSecsRemaining) : EventArgs
{
    /// <summary>
    /// How many API calls remain in this time window
    /// </summary>
    public int ApiCallsRemaining { get; } = apiCallsRemaining;
    
    /// <summary>
    /// How many seconds remain in this time window
    /// </summary>
    public int WindowSecsRemaining { get; } = windowSecsRemaining;
}