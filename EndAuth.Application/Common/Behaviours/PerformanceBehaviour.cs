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
        var result = await next();
        _stopwatch.Stop();
        _logger.LogInformation("Request {requestName} completed in {requestTime}", nameof(TRequest), _stopwatch.ElapsedMilliseconds);
        return result;
    }
}
