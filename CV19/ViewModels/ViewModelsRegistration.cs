using Microsoft.Extensions.DependencyInjection;

namespace CV19.ViewModels;

internal static class ViewModelsRegistration
{
    public static IServiceCollection RegisterViewModels(this IServiceCollection services)
    {
        services.AddSingleton<MainWindowViewModel>();
        services.AddSingleton<CountriesStatisticViewModel>();
        services.AddSingleton<WebServerViewModel>();
        services.AddTransient<StudentManagmentViewModel>();

        return services;
    }
}
