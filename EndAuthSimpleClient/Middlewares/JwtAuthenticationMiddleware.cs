using EndAuthSimpleClient.Attributes;

namespace EndAuthSimpleClient.Middlewares;

public class JwtAuthenticationMiddleware
{
    private readonly RequestDelegate _next;

    public JwtAuthenticationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public Task Invoke(HttpContext httpContext)
    {
        if (httpContext.GetEndpoint()?.Metadata.GetMetadata<JwtAuthenticationAttribute>() is not null)
        {
            if (!string.IsNullOrWhiteSpace(httpContext.Request.Cookies["jwt_token"])
                && !string.IsNullOrWhiteSpace(httpContext.Request.Cookies["refresh_token"])
                && !string.IsNullOrWhiteSpace(httpContext.Request.Cookies["jwt_token_expires"])
                && !string.IsNullOrWhiteSpace(httpContext.Request.Cookies["refresh_token_expires"]))
            {
                //Check if Refresh Token is expired
                DateTimeOffset expirationOfRefreshToken = DateTimeOffset.Parse(httpContext.Request.Cookies["refresh_token_expires"]!);
                if (DateTimeOffset.Now > expirationOfRefreshToken)
                {
                    httpContext.Response.Redirect("/Auth/Login");
                    return Task.CompletedTask;
                }
                //Refresh token is still valid, so we can check if JWT is valid
                DateTimeOffset expirationOfAccessToken = DateTimeOffset.Parse(httpContext.Request.Cookies["jwt_token_expires"]!);
                if (DateTimeOffset.Now > expirationOfAccessToken)
                {
                    httpContext.Response.Redirect("/Auth/Refresh");
                    return Task.CompletedTask;
                }
                return _next(httpContext);
            }
            httpContext.Response.Redirect("/Auth/Login");
            return Task.CompletedTask;
        }
        return _next(httpContext);
    }
}
