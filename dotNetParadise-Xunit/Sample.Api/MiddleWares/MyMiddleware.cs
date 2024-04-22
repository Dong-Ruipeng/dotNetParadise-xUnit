using Microsoft.AspNetCore.Http;

namespace Sample.Api.MiddleWares;

public class MyMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        // 在请求处理前执行的逻辑
        context.Response.ContentType = "text/plain";
        context.Response.Headers.TryAdd("X-Custom-Header", "CustomValue");
        await context.Response.WriteAsync("Test My Middleware");
        // 调用下一个中间件
        await next(context);

        // 在请求处理后执行的逻辑
        await context.Response.WriteAsync("Middleware Test Completed");
    }
}
