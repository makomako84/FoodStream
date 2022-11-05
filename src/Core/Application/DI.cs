using Foodstream.Application.Interfaces;
using Foodstream.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Foodstream.Application;

public static class DI
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IPointService, PointService>();

        return services;
    }
}
