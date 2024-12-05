using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore; // Asegúrate de incluir este namespace

public class GetPlatformStatusesQuery : IRequest<List<PlatformStatusDto>>
{
}

public class GetPlatformStatusesHandler : IRequestHandler<GetPlatformStatusesQuery, List<PlatformStatusDto>>
{
    private readonly PlatformStatusService _platformStatusService;
    private readonly ILogger<GetPlatformStatusesHandler> _logger; // Cambié el tipo de logger a GetPlatformStatusesHandler
    private readonly AppDbContext _context;
    public GetPlatformStatusesHandler(PlatformStatusService platformStatusService, ILogger<GetPlatformStatusesHandler> logger, AppDbContext context)
    {
        _platformStatusService = platformStatusService;
        _logger = logger;
        _context = context;
    }

    public async Task<List<PlatformStatusDto>> Handle(GetPlatformStatusesQuery request, CancellationToken cancellationToken)
    {
        var platforms = new List<(string Name, string Url)>
        {
            ("Mercado Pago", "https://mercadopago.bistrosoft.com/api/check"),
            ("Modo", "https://modo.bistrosoft.com/api/v1/check"),
            ("Multidelivery", "https://multidelivery.bistrosoft.com/api/check"),
            ("Clip", "https://mx-clip.bistrosoft.com/api/v1.0/check")
        };

        var platformStatuses = new List<PlatformStatusDto>();

        foreach (var platform in platforms)
        {
            // Llamada al servicio para obtener el estado de la plataforma
            var response = await _platformStatusService.CheckPlatformStatus(platform);
            
            platformStatuses.Add(new PlatformStatusDto
            {
                PlatformName = response.PlatformName,
                Version = response.Version,
                Status = response.Status
            });
        }
        return platformStatuses;
    }
}
