using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Sample.Repository.Extensions;

namespace dotNetParadise.Bogus;

public class Startup
{

    /// <summary>
    /// ConfigureServices 方法用于配置依赖注入容器（ServiceCollection）。在这个方法中，您可以注册应用程序所需的各种服务、中间件、日志、数据库上下文等等。这个方法通常用于配置应用程序的依赖注入服务。
    /// </summary>
    /// <param name="services"></param>
    /// <param name="context"></param>
    public void ConfigureServices(IServiceCollection services, HostBuilderContext context)
    {
        services.AddEFCoreInMemoryAndRepository();
    }
}
