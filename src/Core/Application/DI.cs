using MakoSystems.Foodstream.Application;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MakoSystems.Foodstream.Application;

public static class DI
{
    public static IServiceCollection AddApplication(this IServiceCollection services,
        ConfigurationManager configuration)
    {
        services
            .Configure<ApplicationSettings>(configuration.GetSection(ApplicationSettings.Position));

        services.AddScoped<IPointService, PointService>();

        return services;
    }
}
