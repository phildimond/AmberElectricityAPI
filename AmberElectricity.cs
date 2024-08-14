using System.Net;
using System.Text.Json;
using AmberElectricityAPI.Models;

namespace AmberElectricityAPI;

public class AmberElectricity
{
    private string _token = string.Empty;
    private string url = "https://api.amber.com.au/v1";

    public string SiteId { get; set; }

    // Constructor
    public AmberElectricity(string token, string siteId )
    {
        _token = token;
        SiteId = siteId;
    }

    // Get a list of sites 
    public Site[]? GetSites()
    {
        (HttpStatusCode statusCode, string responseBody) responseData;

        string body = string.Empty;

        responseData = Network.ProcessRequest(url, "/sites", true, _token);
        
        if (responseData.statusCode != HttpStatusCode.OK) return null;
        
        Site[]? sites = JsonSerializer.Deserialize<Site[]>(responseData.responseBody);

        return sites;
    }
    
    // Usage
    public UsageRecord[]? GetSiteUsage(DateTime startDate, DateTime endDate, int resolution = 30)
    {
        (HttpStatusCode statusCode, string responseBody) responseData;

        string body = string.Empty;
        string apiCall = "/sites/" + SiteId + "/usage" +
                         "?startDate=" + startDate.ToString("yyyy-MM-dd") +
                         "&endDate=" + startDate.ToString("yyyy-MM-dd") +
                         "&resolution=" + resolution; 
        responseData = Network.ProcessRequest(url, apiCall, true, _token);
        
        if (responseData.statusCode != HttpStatusCode.OK) return null;
        
        UsageRecord[]? recs = JsonSerializer.Deserialize<UsageRecord[]>(responseData.responseBody);

        return recs;
    }

    // Site Prices
    public IntervalRecord[]? GetSitePrices(DateTime startDate, DateTime endDate, int resolution = 30)
    {
        (HttpStatusCode statusCode, string responseBody) responseData;

        string body = string.Empty;
        string apiCall = "/sites/" + SiteId + "/prices" +
                         "?startDate=" + startDate.ToString("yyyy-MM-dd") +
                         "&endDate=" + startDate.ToString("yyyy-MM-dd") +
                         "&resolution=" + resolution; 
        responseData = Network.ProcessRequest(url, apiCall, true, _token);
        
        if (responseData.statusCode != HttpStatusCode.OK) return null;
        
        IntervalRecord[]? recs = JsonSerializer.Deserialize<IntervalRecord[]>(responseData.responseBody);

        return recs;
    }

    // Current Prices
    public IntervalRecord[]? GetCurrentPrices(uint nextIntervals, uint previousIntervals, int resolution = 30)
    {
        (HttpStatusCode statusCode, string responseBody) responseData;

        string body = string.Empty;
        string apiCall = "/sites/" + SiteId + "/prices/current";
        Dictionary<string, string?>? paramSet = null;
        if (nextIntervals != 0 || previousIntervals != 0) apiCall += "?'";
        if (nextIntervals != 0) apiCall += "next=" + nextIntervals;
        if (previousIntervals != 0)
        {
            if (nextIntervals != 0) apiCall += "&";
            apiCall += "next=" + previousIntervals;
        }
        
        responseData = Network.ProcessRequest(url, apiCall, true, _token);
        
        if (responseData.statusCode != HttpStatusCode.OK) return null;
        
        IntervalRecord[]? recs = JsonSerializer.Deserialize<IntervalRecord[]>(responseData.responseBody);

        return recs;
    }
    
}