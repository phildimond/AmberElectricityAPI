//+-------------------------------------------------------------------------------------
// 
// Amber Electricity API, (c) Phillip Dimond, 2024
//
// See included licence document.
// 
//+-------------------------------------------------------------------------------------

using System.Net;
using System.Net.Http.Headers;
using System.Text.Json;
using AmberElectricityAPI.Models;
using EnergySuper.EventArgsAndHandlers;

namespace AmberElectricityAPI;

public class AmberElectricity
{
    private string _token;
    private string _url = "https://api.amber.com.au/v1";
    private int? _rateApiCallLimitPerWindow;
    private int? _rateApiCallsRemainingThisWindow;
    private int? _rateSecsToWindowReset;
    private int? _rateMaxCallsPerWindow;
    private int? _rateSecsPerWindow;

    public LowAmberApiCallsRemainingEventHandler? LowAmberApiCallsRemainingEvent;
    
    /// <summary>
    /// Maximum calls allowed in this rate window
    /// </summary>
    public int? RateApiCallLimitPerWindow { get => _rateApiCallLimitPerWindow; }
    
    /// <summary>
    /// Remaining calls allowed in this rate window
    /// </summary>
    public int? RateApiCallsRemainingThisWindow { get => _rateApiCallsRemainingThisWindow; }
    
    /// <summary>
    /// Seconds remaining until this rate window resets
    /// </summary>
    public int? RateSecsToWindowReset { get => _rateSecsToWindowReset; }
    
    /// <summary>
    /// Maximum API calls per rate window
    /// </summary>
    public int? RateMaxCallsPerWindow { get => _rateMaxCallsPerWindow; }
    
    /// <summary>
    /// Rate window length in seconds
    /// </summary>
    public int? RateSecsPerWindow { get => _rateSecsPerWindow; }

    /// <summary>
    /// Amber Site ID
    /// </summary>
    public string SiteId { get; }

    /// <summary>
    /// Sets a threshold that, when the remaining API call allowance in this
    /// time window is reached, a low API Calls Remmaining event will be generated.
    /// Defaults to 10
    /// </summary>
    public int LowApiRemainingAlertThreshold { get; set; } = 10;

    // Constructor
    public AmberElectricity(string token, string siteId )
    {
        _token = token;
        SiteId = siteId;
    }

    /// <summary>
    /// Get a list of sites 
    /// </summary>
    /// <returns>null on failure, an array of sites on success</returns>
    public Site[]? GetSites()
    {
        (HttpStatusCode statusCode, string responseBody, HttpResponseHeaders headers) responseData;

        responseData = Network.ProcessRequest(_url, "/sites", true, _token);
        if (responseData.statusCode != HttpStatusCode.OK) return null;
        Site[]? sites = JsonSerializer.Deserialize<Site[]>(responseData.responseBody);
        ProcessRateHeaders(responseData.headers);

        return sites;
    }

    /// <summary>
    /// Get list of sites asynchronously
    /// </summary>
    /// <returns>null on failure, an array of sites on success</returns>
    public async Task<Site[]?> GetSitesAsync()
    {
        var result = await Task.Run(GetSites);
        return result;
    }
    
    /// <summary>
    /// Get usage records
    /// </summary>
    /// <param name="startDate"></param>
    /// <param name="endDate"></param>
    /// <param name="resolution"></param>
    /// <returns>An array of usage records and the http response code on success. The
    /// usage records will be null if the call fails.</returns>
    public (UsageRecord[]? recs, string httpResponseCode) GetSiteUsage(DateTime startDate, DateTime endDate, int resolution = 30)
    {
        (HttpStatusCode statusCode, string responseBody, HttpResponseHeaders headers) responseData;

        string apiCall = "/sites/" + SiteId + "/usage" +
                         "?startDate=" + startDate.ToString("yyyy-MM-dd") +
                         "&endDate=" + startDate.ToString("yyyy-MM-dd") +
                         "&resolution=" + resolution; 

        responseData = Network.ProcessRequest(_url, apiCall, true, _token);
        if (responseData.statusCode != HttpStatusCode.OK) return (null, responseData.statusCode.ToString());
        UsageRecord[]? recs = JsonSerializer.Deserialize<UsageRecord[]>(responseData.responseBody);
        ProcessRateHeaders(responseData.headers);

        return (recs, responseData.statusCode.ToString());
    }

    /// <summary>
    /// Get usage records asynchronously
    /// </summary>
    /// <param name="startDate"></param>
    /// <param name="endDate"></param>
    /// <param name="resolution"></param>
    /// <returns>An array of usage records and the http response code on success. The
    /// usage records will be null if the call fails.</returns>
    public async Task<(UsageRecord[]? recs, string httpResponseCode)> 
        GetSiteUsageAsync(DateTime startDate, DateTime endDate, int resolution = 30)
    {
        var result = await 
            Task.Run<(UsageRecord[]?, string)>(() => GetSiteUsage(startDate, endDate, resolution));
        return result;
    }
    
    /// <summary>
    /// Get Site Prices
    /// </summary>
    /// <param name="startDate"></param>
    /// <param name="endDate"></param>
    /// <param name="resolution"></param>
    /// <returns>An array of price records and the http status code on success. On failure the records will be null.</returns>
    public (IntervalRecord[]? recs, string httpStatusCode) GetSitePrices(DateTime startDate, DateTime endDate, int resolution = 30)
    {
        (HttpStatusCode statusCode, string responseBody, HttpResponseHeaders headers) responseData;

        string apiCall = "/sites/" + SiteId + "/prices" +
                         "?startDate=" + startDate.ToString("yyyy-MM-dd") +
                         "&endDate=" + startDate.ToString("yyyy-MM-dd") +
                         "&resolution=" + resolution;
        
        responseData = Network.ProcessRequest(_url, apiCall, true, _token);
        if (responseData.statusCode != HttpStatusCode.OK) return (null, responseData.statusCode.ToString());
        IntervalRecord[]? recs = JsonSerializer.Deserialize<IntervalRecord[]>(responseData.responseBody);
        ProcessRateHeaders(responseData.headers);

        return (recs, responseData.statusCode.ToString());
    }

    /// <summary>
    /// Get Site Prices Asynchronously
    /// </summary>
    /// <param name="startDate"></param>
    /// <param name="endDate"></param>
    /// <param name="resolution"></param>
    /// <returns>An array of price records and the http status code on success. On failure the records will be null.</returns>
    public async Task<(IntervalRecord[]? recs, string httpStatusCode)> 
        GetSitePricesAsync(DateTime startDate, DateTime endDate, int resolution = 30)
    {
        var result = await 
            Task.Run<(IntervalRecord[]?, string)>(() => GetSitePrices(startDate, endDate, resolution));
        return result;
    }
    
    /// <summary>
    /// Get Current Prices
    /// </summary>
    /// <param name="nextIntervals"></param>
    /// <param name="previousIntervals"></param>
    /// <param name="resolution"></param>
    /// <returns>A list of records on success. On failure, the records list will be
    /// null and the httpStatusCode will indicate the error from the API</returns>
    public (IntervalRecord[]? records, string httpStatusCode) 
        GetCurrentPrices(uint nextIntervals, uint previousIntervals, int resolution = 30)
    {
        (HttpStatusCode statusCode, string responseBody, HttpResponseHeaders headers) responseData;

        string apiCall = "/sites/" + SiteId + "/prices/current";

        if (nextIntervals != 0 || previousIntervals != 0) apiCall += "?";
        if (nextIntervals != 0) apiCall += "next=" + nextIntervals;
        if (previousIntervals != 0)
        {
            if (nextIntervals != 0) apiCall += "&";
            apiCall += "previous=" + previousIntervals;
        }
        
        responseData = Network.ProcessRequest(_url, apiCall, true, _token);
        if (responseData.statusCode != HttpStatusCode.OK) return (null, responseData.statusCode.ToString());
        IntervalRecord[]? recs = JsonSerializer.Deserialize<IntervalRecord[]>(responseData.responseBody);
        ProcessRateHeaders(responseData.headers);

        return (recs, responseData.statusCode.ToString());
    }

    /// <summary>
    /// Get Current Prices Asynchronously
    /// </summary>
    /// <param name="nextIntervals"></param>
    /// <param name="previousIntervals"></param>
    /// <param name="resolution"></param>
    /// <returns>A list of records on success. On failure, the records list will be
    /// null and the httpStatusCode will indicate the error from the API</returns>
    public async Task<(IntervalRecord[]? records, string httpStatusCode)>
        GetCurrentPricesAsync(uint nextIntervals, uint previousIntervals, int resolution = 30)
    {
        var result = await 
            Task.Run<(IntervalRecord[]?, string)>(() => GetCurrentPrices(nextIntervals, previousIntervals, resolution));
        return result;
    }
    
    /// <summary>
    /// Renewables
    /// </summary>
    /// <param name="state"></param>
    /// <param name="nextIntervals"></param>
    /// <param name="previousIntervals"></param>
    /// <param name="resolution"></param>
    /// <returns>An array of renewables records plus the received status code. The records array will be
    /// null in case of a failure.</returns>
    /// <exception cref="NotImplementedException"></exception>
    /// <exception cref="Exception"></exception>
    public (RenewablesRecord[]? records, string httpStatusCode) GetRenewables(RenewablesStateEnum state, uint nextIntervals, 
                                             uint previousIntervals, int resolution = 30)
    {
        (HttpStatusCode statusCode, string responseBody, HttpResponseHeaders headers) responseData;

        string stateString = state switch
        {
            RenewablesStateEnum.Unknown => throw new NotImplementedException("Unknown renewable state"),
            RenewablesStateEnum.NewSouthWales => "nsw",
            RenewablesStateEnum.Queensland => "qld",
            RenewablesStateEnum.Victoria => "vic",
            RenewablesStateEnum.SouthAustralia => "sa",
            _ => throw new Exception("Invalid renewables state enum value")
        };
        string apiCall = "/state/" + stateString + "/renewables/current";
        
        if (nextIntervals != 0 || previousIntervals != 0) apiCall += "?";
        if (nextIntervals != 0) apiCall += "next=" + nextIntervals;
        if (previousIntervals != 0)
        {
            if (nextIntervals != 0) apiCall += "&";
            apiCall += "previous=" + previousIntervals;
        }
        
        responseData = Network.ProcessRequest(_url, apiCall, true, _token);
        if (responseData.statusCode != HttpStatusCode.OK) return (null, responseData.statusCode.ToString());
        RenewablesRecord[]? recs = JsonSerializer.Deserialize<RenewablesRecord[]?>(responseData.responseBody);
        ProcessRateHeaders(responseData.headers);

        return (recs, responseData.statusCode.ToString());
    }
    
    /// <summary>
    /// Get Renewables Asynchronously
    /// </summary>
    /// <param name="state"></param>
    /// <param name="nextIntervals"></param>
    /// <param name="previousIntervals"></param>
    /// <param name="resolution"></param>
    /// <returns>An array of renewables records plus the received status code. The records array will be
    /// null in case of a failure.</returns>
    public async Task<(RenewablesRecord[]? records, string httpStatusCode)>
        GetRenewablesAsync(RenewablesStateEnum state, uint nextIntervals, uint previousIntervals, int resolution = 30)
    {
        var result = await Task.Run<(RenewablesRecord[]?, string)>(() => 
            GetRenewables(state, nextIntervals, previousIntervals, resolution)
            );
        return result;
    }

    /// <summary>
    /// Process the API rate ,limitation information in the returned headers
    /// </summary>
    /// <param name="headers">Headers to process</param>
    private void ProcessRateHeaders(HttpResponseHeaders headers)
    {
        foreach (KeyValuePair<string, IEnumerable<string>> header in headers)
        {
            switch (header.Key)
            {
                case "RateLimit-Remaining": _rateApiCallsRemainingThisWindow = Convert.ToInt32(header.Value.Single()); break;
                case "RateLimit-Limit": _rateApiCallLimitPerWindow = Convert.ToInt32(header.Value.Single()); break;
                case "RateLimit-Reset": _rateSecsToWindowReset = Convert.ToInt32(header.Value.Single()); break;
                case "RateLimit-Policy": // RateLimit-Policy: 50;w=300
                    var s = header.Value.Single();
                    _rateMaxCallsPerWindow = Convert.ToInt32(s.Split(';')[0]); 
                    _rateSecsPerWindow = Convert.ToInt32(s.Split(';')[1].Split('=')[1]);
                    break;
            }
        }

        // Raise a threshold low event
        if (_rateApiCallsRemainingThisWindow < LowApiRemainingAlertThreshold)
        {
            if (LowAmberApiCallsRemainingEvent != null && _rateApiCallsRemainingThisWindow != null && _rateSecsToWindowReset != null) 
                LowAmberApiCallsRemainingEvent.Invoke(this, 
                    new LowAmberApiCallsRemainingEventArgs((int)_rateApiCallsRemainingThisWindow, (int)_rateSecsToWindowReset));
        }
    }
}