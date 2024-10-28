using CodeBridgeTask.Controllers;
using CodeBridgeTask.MiddleWare.RequestsHandle;
using Microsoft.AspNetCore.Http;

namespace CodeBridgeTask.Tests;

public class RequestHandlerTests
{
    private readonly RequestDelegate _next;
    private RequestsHandler _requestsHandler;

    public RequestHandlerTests()
    {
        _next = context => Task.CompletedTask;
    }
    
    [Fact]
    public async Task Send11Requests_ShouldReturn429ForTheLastRequest()
    {
        _requestsHandler = new RequestsHandler(_next);
        // Arrange
        var context = new DefaultHttpContext();
        int requestsCount = 11;

        // Act
        for (int i = 0; i < requestsCount; i++)
        {
            await _requestsHandler.InvokeAsync(context);
        }
        
        // Assert
        Assert.Equal(StatusCodes.Status429TooManyRequests, context.Response.StatusCode);
    }    
    [Fact]
    public async Task Send10Requests_ShouldReturnOkForEveryRequest()
    {
        // Arrange
        List<int> statusCodes = [];
        _requestsHandler = new RequestsHandler(_next);
        var context = new DefaultHttpContext();
        int requestsCount = 10;

        // Act
        for (int i = 0; i < requestsCount; i++)
        {
            await _requestsHandler.InvokeAsync(context);
            statusCodes.Add(context.Response.StatusCode);
        }
        
        // Assert
        foreach (var statusCode in statusCodes)
        {
            Assert.Equal(200, statusCode);
        }
    }
}
