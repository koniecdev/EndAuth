using MediatR.Pipeline;
using Microsoft.Extensions.Logging;

namespace EndAuth.Application.Common.Behaviours;

public class LoggingBehaviour<TRequest> : IRequestPreProcessor<TRequest> where TRequest : notnull
{
    private readonly ILogger<TRequest> _logger;

    public LoggingBehaviour(ILogger<TRequest> logger)
    {
        _logger = logger;
    }
    public Task Process(TRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Request registered: {Name}, {@Request}.", typeof(TRequest).Name, request);
        return Task.CompletedTask;
    }
}
