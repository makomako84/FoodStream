using MakoSystems.Foodstream.Application;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MakoSystems.Foodstream.Postgre;

public static class DI
{
    private const string MigrationsAssembly = "Postgre";

    public static IServiceCollection AddPostgre(
        this IServiceCollection services,
        ConfigurationManager configuration)
    {
        services.AddDbContext<PostgreContext>(
            options =>
            {
                options.UseNpgsql(
                    configuration["PostgreConnection:ConnectionString"],
                    x => x.MigrationsAssembly(MigrationsAssembly));
                options.EnableDetailedErrors();
                options.EnableSensitiveDataLogging();
            });

        services.AddScoped<IPointRepository, PointRepository>();
        return services;
    }

    //public static void UpdateDatabase(this IApplicationBuilder app)
    //{
    //    using var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
    //    var context = serviceScope.ServiceProvider.GetService<PostgreContext>();
    //    context.Database.Migrate();

    //    using (var conn = (NpgsqlConnection)context.Database.GetDbConnection())
    //    {
    //        conn.Open();
    //        conn.ReloadTypes();
    //    }
    //}
}
