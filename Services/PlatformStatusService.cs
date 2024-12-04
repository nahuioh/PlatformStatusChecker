using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

public class PlatformStatusService
{
    private readonly HttpClient _httpClient;

    public PlatformStatusService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<string> CheckPlatformStatus(string url)
    {
        var response = await _httpClient.GetStringAsync(url);
        var jsonResponse = JObject.Parse(response);
        var statusCode = jsonResponse["responseCode"]?.ToString();

        return statusCode == "0" ? "OK" : "ERROR";
    }
}
