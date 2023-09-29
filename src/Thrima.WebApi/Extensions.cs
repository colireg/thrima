using Microsoft.EntityFrameworkCore;
using Thrima.Infrastructure.Persistence;

namespace Thrima.WebApi;

public static class Extensions
{
    public static WebApplication ApplyMigrations(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        using var appDbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        appDbContext.Database.Migrate();
        return app;
    }
}
