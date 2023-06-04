using LanguageExt.Pipes;

namespace EndAuthSimpleClient.Middlewares;

public class RequestMiddleware
{
    private readonly RequestDelegate _next;

    public RequestMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public Task Invoke(HttpContext httpContext)
    {
        if (!string.IsNullOrWhiteSpace(httpContext.Request.Cookies["jwt_token"]) && !string.IsNullOrWhiteSpace(httpContext.Request.Cookies["refresh_token"]))
        {
            //I think we have to return also expire date of JWT and Refresh, and check it here, instead of relying of responses from api after calling it.
        }

        return _next(httpContext);
    }
}
