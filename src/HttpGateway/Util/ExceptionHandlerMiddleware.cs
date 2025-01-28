using Grpc.Core;
using System.Net;

namespace HttpGateway.Util;

#pragma warning disable IDE0072 // switch

public class ExceptionHandlerMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (RpcException ex)
        {
            await HandleGrpcExceptionAsync(context, ex);
        }
        catch (Exception e)
        {
            await HandleExceptionAsync(context, e);
        }
    }

    private async Task HandleGrpcExceptionAsync(HttpContext context, RpcException exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = exception.StatusCode switch
        {
            StatusCode.NotFound => (int)HttpStatusCode.NotFound,
            StatusCode.InvalidArgument => (int)HttpStatusCode.BadRequest,
            StatusCode.PermissionDenied => (int)HttpStatusCode.Forbidden,
            StatusCode.Unauthenticated => (int)HttpStatusCode.Unauthorized,
            StatusCode.Internal => (int)HttpStatusCode.InternalServerError,
            _ => (int)HttpStatusCode.InternalServerError,
        };

        string message = $"""
                          Exception occured while processing request, status = {exception.StatusCode}, message = {exception.Status.Detail}";
                          """;
        await context.Response.WriteAsJsonAsync(new { message });
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        string message = $"""
                          Exception occured while processing request, type = {exception.GetType().Name}, message = {exception.Message}";
                          """;
        await context.Response.WriteAsJsonAsync(new { message });
    }
}