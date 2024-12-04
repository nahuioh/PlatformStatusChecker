using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;

public class PlatformStatusService
{
    private readonly HttpClient _httpClient;
    private readonly AppDbContext _context;
    private readonly ILogger<GetPlatformStatusesHandler> _logger;

    public PlatformStatusService(HttpClient httpClient, AppDbContext context, ILogger<GetPlatformStatusesHandler> logger)
    {
        _httpClient = httpClient;
        _context = context;
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
                Version = version ?? "Unknown", // Maneja nulos en caso de que falte
                Status = status == "0" ? "OK" : "ERROR",  // Mapea el código de estado
                Url = platformTemp.Url,
            };

            // Guardamos el estado de la plataforma en la base de datos en memoria
            _context.PlatformStatuses.Add(platform);
            await _context.SaveChangesAsync();

            return platform;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener el estado de la plataforma: {Url}", platformTemp.Url);
            return new PlatformStatus
            {
                PlatformName = platformTemp.Name,
                Version = "N/A",   // Indica que no se pudo obtener la versión
                Status = "ERROR"   // Indica que hubo un error en el chequeo
            };
        }
    }

}
