using CV19.Services.Interfaces;
using CV19.Services.Students;
using Microsoft.Extensions.DependencyInjection;

namespace CV19.Services;

internal static class ServiceRegistration
{
    public static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        services.AddSingleton<IDataService, DataService>();
        services.AddTransient<IAsyncDataService, AsyncDataService>();
        services.AddTransient<IWebServerService, HttpListenerWebService>();
        
        services.AddSingleton<StudentsRepository>();
        services.AddSingleton<GroupsRepository>();
        services.AddSingleton<StudentsManager>();

        services.AddTransient<IUserDialogService, WindowsUserDialogService>();

        return services;
    }
}