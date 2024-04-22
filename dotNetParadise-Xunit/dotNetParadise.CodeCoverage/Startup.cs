using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Sample.Repository.Extensions;
using Xunit.DependencyInjection.Logging;

namespace dotNetParadise.CodeCoverage;

public class Startup
{
    //CreateHostBuilder 方法用于创建应用程序的主机构建器（HostBuilder）。在这个方法中，您可以配置主机的各种参数、服务、日志、环境等。这个方法通常用于配置主机构建器的各种属性，以便在应用程序启动时使用。
    //public IHostBuilder CreateHostBuilder([AssemblyName assemblyName]) { }

    /// <summary>
    /// ConfigureHost 方法用于配置主机构建器。在这个方法中，您可以对主机进行一些自定义的配置，比如设置环境、使用特定的配置源等
    /// </summary>
    /// <param name="hostBuilder"></param>
    public void ConfigureHost(IHostBuilder hostBuilder) { }


    /// <summary>
    /// ConfigureServices 方法用于配置依赖注入容器（ServiceCollection）。在这个方法中，您可以注册应用程序所需的各种服务、中间件、日志、数据库上下文等等。这个方法通常用于配置应用程序的依赖注入服务。
    /// </summary>
    /// <param name="services"></param>
    /// <param name="context"></param>
    public void ConfigureServices(IServiceCollection services, HostBuilderContext context)
    {
        services.AddEFCoreInMemoryAndRepository();
        services.AddLogging(lb => lb.AddXunitOutput());
        //设置环境
        context.HostingEnvironment.EnvironmentName = "test";
        //使用配置
        context.Configuration.GetChildren();
    }

    /// <summary>
    /// ConfigureServices 中配置的服务可以在 Configure 方法中指定。如果已经配置的服务在 Configure 方法的参数中可用，它们将会被注入
    /// </summary>
    public void Configure()
    {

    }

}
