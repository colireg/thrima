using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Thrima.Application.Services;
using Thrima.Infrastructure.Persistence;

namespace Thrima.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(
            dbContextOptions => dbContextOptions.UseSqlServer(configuration.GetConnectionString("SqlServer"),
            sqlServeOptions => sqlServeOptions.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)));

        services.AddScoped<IAppDbContext, AppDbContext>();

        return services;
    }
}
