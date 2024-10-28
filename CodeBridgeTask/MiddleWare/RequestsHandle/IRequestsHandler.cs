namespace CodeBridgeTask.MiddleWare.RequestsHandle;

public interface IRequestsHandler
{
    Task InvokeAsync(HttpContext context);
}