//+-------------------------------------------------------------------------------------
// 
// Amber Electricity API, (c) Phillip Dimond, 2024
//
// See included licence document.
// 
//+-------------------------------------------------------------------------------------


using System.Net;
using Microsoft.AspNetCore.WebUtilities;

namespace AmberElectricityAPI;

public class Network
{
    // ====================================================
    // Perform a GET API transaction
    // ====================================================

    public static (HttpStatusCode statusCode, string responseBody) 
        ProcessRequest(string baseUrl, string api, bool tokenNeeded, string token = "")
    {
        
        var clientHandler = new HttpClientHandler
        {
            UseCookies = false,
        };

        var client = new HttpClient(clientHandler);
        
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri(baseUrl + api)
        };
        
        if (tokenNeeded)
        {
            request.Headers.Add("Authorization", "Bearer " + token);
        }
        
        using (var response = client.Send(request))
        {
            var result = response.Content.ReadAsStream();
            StreamReader reader = new StreamReader(result);
            return (response.StatusCode, reader.ReadToEnd());
        } 
    }

    // ====================================================
    // Perform a POST API transaction
    // ====================================================

    public static (HttpStatusCode statusCode, string responseBody, IEnumerable<string> cookies) 
        PostData(string baseUrl, string api, string body, bool tokenNeeded, string token = "")
    {
        var clientHandler = new HttpClientHandler
        {
            UseCookies = false,
            ClientCertificateOptions = ClientCertificateOption.Manual,
            ServerCertificateCustomValidationCallback = (httpRequestMessage, cert, cetChain, policyErrors) => { return true; } // ignore SSL cert errors
        };

        var client = new HttpClient(clientHandler);

        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri(baseUrl + api),
            Headers =
            {
                { "Authorization", "Bearer " + token }
            },
            Content = new StringContent(body)
        };

        using (var response = client.Send(request))
        {
            var cookies = 
                response.Headers.SingleOrDefault(header => header.Key == "Set-Cookie").Value;
           
            var result = response.Content.ReadAsStream();
            StreamReader reader = new StreamReader(result);
            return (response.StatusCode, reader.ReadToEnd(), cookies); 
        }
    }
}