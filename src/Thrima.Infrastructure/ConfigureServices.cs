using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Thrima.Application.Services;
using Thrima.Infrastructure.Persistence;

namespace Thrima.Infrastructure;

public static class ConfigureServices
{
    public static void AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(
            // dbContextOptions => dbContextOptions.UseSqlServer(configuration.GetConnectionString("SqlServer"),
            dbContextOptions => dbContextOptions.UseInMemoryDatabase(configuration.GetConnectionString("Memory") ?? "thrima")
            // sqlServeOptions => sqlServeOptions.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName))
            );

        services.AddScoped<IAppDbContext, AppDbContext>();
    }
}
