using CV19.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace CV19.Services;

internal static class ServiceRegistration
{
    public static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        services.AddSingleton<IDataService, DataService>();
        services.AddTransient<IAsyncDataService, AsyncDataService>();

        return services;
    }
}