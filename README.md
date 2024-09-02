# Amber Electricity API Implementation

> A simple implementation in C# 8.0 of the Amber Electricity API as at August 2024
> 
> The Amber API documentation is found at https://app.amber.com.au/developers/

>License

See the included MIT license document
 
>How to get it
> 
- Clone the repository
- issue the dotnet publish command
- You'll find the DLL at the listed location
- Reference the DLL

>Usage:

Add the following using:<code> using AmberElectricityAPI;</code>

The constructor call is simple:

<code> AmberElectricity amberElectricity = new AmberElectricity(AmberToken, AmberSiteId);</code>

The <code>AmberToken</code> value is obtained from https://app.amber.com.au/developers/ 
- make sure you record this somewhere as you can't get it back later!

The <code>AmberSiteId</code> value is obtained from the <code>GetSites()</code> API call.
- You can get your site ID from the developer page above by trying the Sites call, or
- You can instantiate an instance of this API with an empty site ID, call <code>GetSites()</code>, record the 
SiteID and then instantiate a new instance using the site ID for further calls.

Both synchronous and asynchronous versions of the calls are included.

> Changes

2nd Sept 2024: Non-breaking: added Amber API rate limit processing on call responses. Most recent rate values are available as properties of the AmberElectricityAPI class. They will be null if they have never been populated.
 