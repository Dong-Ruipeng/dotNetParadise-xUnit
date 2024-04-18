using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Sample.Repository.Contexts;
namespace dotNetParadise.IntegrationTest;

public class SampleApiWebAppFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {

        builder.ConfigureServices((context, services) =>
        {
            var descriptor = new ServiceDescriptor(
                typeof(DbContextOptions<SampleDbContext>),
                serviceProvider => DbContextFactory<SampleDbContext>(serviceProvider, (sp, o) =>
                {
                    o.UseInMemoryDatabase("TestDB");
                }),
                 ServiceLifetime.Scoped);

            services.Replace(descriptor);
        });
        builder.UseEnvironment("Production");
        base.ConfigureWebHost(builder);
    }

    private static DbContextOptions<TContext> DbContextFactory<TContext>(IServiceProvider applicationServiceProvider,
      Action<IServiceProvider, DbContextOptionsBuilder> optionsAction)
      where TContext : DbContext
    {
        var builder = new DbContextOptionsBuilder<TContext>(
            new DbContextOptions<TContext>(new Dictionary<Type, IDbContextOptionsExtension>()));

        builder.UseApplicationServiceProvider(applicationServiceProvider);

        optionsAction?.Invoke(applicationServiceProvider, builder);

        return builder.Options;
    }


    public HttpClient Client()
    {
        return CreateDefaultClient();
    }
}


