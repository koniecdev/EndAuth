namespace EndAuth.Infrastructure.ExceptionsHandling;

public record FailureResponse(int StatusCode, string Message);