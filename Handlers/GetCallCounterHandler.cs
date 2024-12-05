using MediatR;
using Microsoft.EntityFrameworkCore;

public class GetCallCounterQuery : IRequest<int>
{
    public required string  EndpointName { get; set; }
    public required string Action { get; set; }
}
public class GetCallCounterHandler : IRequestHandler<GetCallCounterQuery, int>
{
    private readonly AppDbContext _context;

    public GetCallCounterHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(GetCallCounterQuery request, CancellationToken cancellationToken)
    {
        if (request.Action == "increment")
        {
            // Incrementar el contador
            var counter = await _context.ApiCallCounters
                .FirstOrDefaultAsync(c => c.EndpointName == request.EndpointName, cancellationToken);

            if (counter == null)
            {
                counter = new ApiCallCounter
                {
                    EndpointName = request.EndpointName,
                    CallCount = 1
                };
                _context.ApiCallCounters.Add(counter);
            }
            else
            {
                counter.CallCount++;
                _context.ApiCallCounters.Update(counter);
            }

            await _context.SaveChangesAsync(cancellationToken);
        }

        var result = await _context.ApiCallCounters
            .FirstOrDefaultAsync(c => c.EndpointName == request.EndpointName, cancellationToken);

        return result?.CallCount ?? 0;
    }
}
