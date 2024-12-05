using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

public class PlatformStatusService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<GetPlatformStatusesHandler> _logger;
    public PlatformStatusService(HttpClient httpClient, ILogger<GetPlatformStatusesHandler> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<PlatformStatus> CheckPlatformStatus((string Name, string Url) platformTemp)
    {
        try
        {
            var response = await _httpClient.GetStringAsync(platformTemp.Url);
            var jsonResponse = JObject.Parse(response);
            var status = jsonResponse["responseCode"]?.ToString();
            var version = jsonResponse["version"]?.ToString();

           _logger.LogInformation(jsonResponse.ToString());

            var platform = new PlatformStatus
            {
                PlatformName = platformTemp.Name,
                Version = version ?? "Unknown",
                Status = status == "0" ? "OK" : "ERROR",
                Url = platformTemp.Url,
            };

            return platform;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener el estado de la plataforma: {Url}", platformTemp.Url);
            return new PlatformStatus
            {
                PlatformName = platformTemp.Name,
                Version = "N/A",
                Status = "ERROR"
            };
        }
    }
}
