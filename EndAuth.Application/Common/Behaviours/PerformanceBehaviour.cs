using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace EndAuth.Application.Common.Behaviours;

public class PerformanceBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    private readonly ILogger<TRequest> _logger;
    private readonly Stopwatch _stopwatch;
    public PerformanceBehaviour(ILogger<TRequest> logger)
    {
        _logger = logger;
        _stopwatch = new();
    }
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        _stopwatch.Start();
        TResponse? response = await next();
        _stopwatch.Stop();

        if (_stopwatch.ElapsedMilliseconds > 500)
        {
            _logger.LogWarning("Long Running Request: {Name} ({ElapsedMilliseconds} milliseconds) {@Request}",
                typeof(TRequest).Name, _stopwatch.ElapsedMilliseconds, request);
        }
        return response;
    }
}
