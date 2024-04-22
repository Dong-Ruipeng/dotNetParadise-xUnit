using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.AspNetCore.Hosting;
using Sample.Api.MiddleWares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
namespace dotNetParadise.MiddlerWareTest;

public class MyMiddlewareTest
{
    [Fact]
    public async Task MyMiddleware_ReturnsExpectedResponse()
    {
        // Arrange
        using var host = await new HostBuilder()
            .ConfigureWebHost(webBuilder =>
            {
                webBuilder
                    .UseTestServer()
                    .ConfigureServices(services =>
                    {
                        // 在这里可以注入服务
                    })
                    .Configure(app =>
                    {
                        app.UseMiddleware<MyMiddleware>();
                    });
            })
            .StartAsync();

        // Act
        var response = await host.GetTestClient().GetAsync("/");

        // Assert
        response.EnsureSuccessStatusCode(); // 确保请求成功
                                            // 验证响应头部是否包含自定义头部  
        var customHeader = response.Headers.GetValues("X-Custom-Header").FirstOrDefault();
        Assert.Equal("CustomValue", customHeader);
        var responseContent = await response.Content.ReadAsStringAsync();
        Assert.Contains("Test My Middleware", responseContent);
        Assert.Contains("Middleware Test Completed", responseContent);

    }

    [Fact]
    public async Task TestMiddleware_ExpectedResponse()
    {
        using var host = await new HostBuilder()
            .ConfigureWebHost(webBuilder =>
            {
                webBuilder
                    .UseTestServer()
                    .ConfigureServices(services =>
                    {
                    })
                    .Configure(app =>
                    {
                        app.UseMiddleware<MyMiddleware>();
                    });
            })
            .StartAsync();

        var server = host.GetTestServer();
        server.BaseAddress = new Uri("http://localhost");

        var context = await server.SendAsync(c =>
        {
            c.Request.Method = HttpMethods.Get;
            c.Request.Path = "/";
            c.Request.Host = new HostString("localhost");
            c.Request.Scheme = "http";
        });
        //act
        Assert.True(context.RequestAborted.CanBeCanceled);
        Assert.Equal(HttpProtocol.Http11, context.Request.Protocol);
        // 验证响应  
        Assert.Equal(StatusCodes.Status200OK, context.Response.StatusCode);
        var customHeader = context.Response.Headers["X-Custom-Header"].ToString();
        Assert.Equal("CustomValue", customHeader);
    }


    [Fact]
    public async Task TestWithEndpoint_ExpectedResponse()
    {
        using var host = await new HostBuilder()
            .ConfigureWebHost(webBuilder =>
            {
                webBuilder
                    .UseTestServer()
                    .ConfigureServices(services =>
                    {
                        services.AddRouting();
                    })
                    .Configure(app =>
                    {
                        app.UseRouting();
                        //app.UseMiddleware<MyMiddleware>();
                        app.UseEndpoints(endpoints =>
                        {
                            endpoints.MapGet("/hello", () =>
                                TypedResults.Text("Hello Tests"));
                        });
                    });
            })
            .StartAsync();

        var client = host.GetTestClient();

        var response = await client.GetAsync("/hello");

        Assert.True(response.IsSuccessStatusCode);
        var responseBody = await response.Content.ReadAsStringAsync();
        Assert.Equal("Hello Tests", responseBody);

    }
}