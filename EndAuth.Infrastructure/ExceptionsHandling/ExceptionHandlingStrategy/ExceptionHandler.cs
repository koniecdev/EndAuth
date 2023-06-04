﻿namespace EndAuth.Infrastructure.ExceptionsHandling.ExceptionHandlingStrategy;

public class ExceptionHandler
{
    private readonly IExceptionHandlerStrategy _exceptionHandlerStrategy;

    public ExceptionHandler(IExceptionHandlerStrategy exceptionHandlerStrategy)
    {
        _exceptionHandlerStrategy = exceptionHandlerStrategy;
    }

    public int Handle()
    {
        return _exceptionHandlerStrategy.Handle();
    }
}
