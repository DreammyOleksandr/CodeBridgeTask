namespace CodeBridgeTask.MiddleWare.RequestsHandle;

public class RequestsHandler : IRequestsHandler
{
    private readonly RequestDelegate _next;
    private readonly int _maxRequests;
    private int _requestCount;
    private DateTime _timePeriod;
    

    public RequestsHandler(RequestDelegate next)
    {
        _next = next;
        _maxRequests = 10;
        _timePeriod = DateTime.UtcNow.AddSeconds(1);
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var currentTime = DateTime.UtcNow;

        if (currentTime >= _timePeriod)
        {
            Interlocked.Exchange(ref _requestCount, 0);
            _timePeriod = currentTime.AddSeconds(1);
        }

        if (Interlocked.Increment(ref _requestCount) > _maxRequests)
        {
            context.Response.StatusCode = StatusCodes.Status429TooManyRequests;
            await context.Response.WriteAsync("Too many requests. Try again later.");
            return;
        }

        await _next(context);
    }
}