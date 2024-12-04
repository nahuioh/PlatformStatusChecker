using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

public class GetPlatformStatusesQuery : IRequest<List<PlatformStatusDto>>
{
}

public class GetPlatformStatusesHandler : IRequestHandler<GetPlatformStatusesQuery, List<PlatformStatusDto>>
{
    private readonly PlatformStatusService _platformStatusService;

    public GetPlatformStatusesHandler(PlatformStatusService platformStatusService)
    {
        _platformStatusService = platformStatusService;
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
            var status = await _platformStatusService.CheckPlatformStatus(platform.Url);
            platformStatuses.Add(new PlatformStatusDto
            {
                PlatformName = platform.Name,
                Version = "1.0.2", // Versión estática por ahora
                Status = status
            });
        }

        return platformStatuses;
    }
}