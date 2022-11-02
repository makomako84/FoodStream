using Foodstream.Persistence.Postgre;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Npgsql;

namespace Foodstream.Persistence;

public static class StartupExtension
{
    private const string MigrationsAssembly = "Postgre";

    public static IServiceCollection AddPostgreCoreContext(this IServiceCollection services, string connectionString)
    {
        return services.AddDbContext<PostgreContext>(
            options =>
            {
                options.UseNpgsql(
                    connectionString,
                    x => x.MigrationsAssembly(MigrationsAssembly));
                options.EnableDetailedErrors();
                options.EnableSensitiveDataLogging();
            });
    }

    public static void UpdateDatabase(this IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
        var context = serviceScope.ServiceProvider.GetService<PostgreContext>();
        context.Database.Migrate();

        using (var conn = (NpgsqlConnection)context.Database.GetDbConnection())
        {
            conn.Open();
            conn.ReloadTypes();
        }
    }
}
