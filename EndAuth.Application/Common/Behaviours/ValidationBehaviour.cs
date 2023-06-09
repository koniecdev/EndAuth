﻿using Microsoft.Extensions.Logging;

namespace EndAuth.Application.Common.Behaviours;

public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;
    private readonly ILogger<TRequest> _logger;
    public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators, ILogger<TRequest> logger)
    {
        _validators = validators;
        _logger = logger;
    }
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var context = new ValidationContext<TRequest>(request);

        var failures = _validators
            .Select(m => m.Validate(context))
            .SelectMany(m => m.Errors)
            .Where(m => m != null)
            .ToList();

        if(failures.Count != 0)
        {
            _logger.LogError("Validation failed at request: {requestName}. Following errors are: {errorList}", nameof(TRequest), string.Join(",", failures));
            throw new ValidationException(failures);
        }

        return await next();
    }
}
