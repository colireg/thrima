namespace Thrima.WebApi;

public static class ConfigureServices
{
    public static void AddWebApi(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
    }
}
