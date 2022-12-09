using Foodstream.Application.Interfaces;
using Foodstream.Application.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Foodstream.Application;

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
