using Microsoft.Extensions.DependencyInjection;
using FluentValidation;

namespace Thrima.Application;

public static class ConfigureServices
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = typeof(ConfigureServices).Assembly;
        services.AddAutoMapper(assembly);
        services.AddMediatR(config => config.RegisterServicesFromAssembly(assembly));
        services.AddValidatorsFromAssembly(assembly);

        return services;
    }
}
