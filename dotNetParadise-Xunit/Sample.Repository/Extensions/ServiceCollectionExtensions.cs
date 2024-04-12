using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Sample.Repository.Contexts;
using Sample.Repository.Repositories;

namespace Sample.Repository.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddEFCoreInMemoryAndRepository(this IServiceCollection services)
    {
        services.AddScoped<IStaffRepository, StaffRepository>();
        services.AddDbContext<SampleDbContext>(options => options.UseInMemoryDatabase("sample").EnableSensitiveDataLogging(), ServiceLifetime.Scoped);
        return services;
    }
}
