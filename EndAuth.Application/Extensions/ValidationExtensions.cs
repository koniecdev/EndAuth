using EndAuth.Application.Common.Behaviours;
using Microsoft.Extensions.DependencyInjection;

namespace EndAuth.Application.Extensions;

public static class ValidationExtensions
{
    public static MediatRServiceConfiguration AddValidation<TRequest, TResponse>(this MediatRServiceConfiguration config) where TRequest : notnull
    {
        return config.AddBehavior<IPipelineBehavior<TRequest, Result<TResponse>>, ValidationBehaviour<TRequest, TResponse>>();
    }
}