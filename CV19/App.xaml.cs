﻿using CV19.Services;
using CV19.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;
using System.Windows;

namespace CV19;

public partial class App : Application 
{
    public static bool IsDesignMode {  get; private set; } = true;
    private static IHost _host;
    public static IHost Host => _host ??= Program.CreateHostBuilder(Environment.GetCommandLineArgs()).Build();

    internal static void ConfigureServices(HostBuilderContext host, IServiceCollection services)
    {
        services.AddSingleton<DataService>();
        services.AddSingleton<CountriesStatisticViewModel>();
    }

    protected override async void OnStartup(StartupEventArgs e)
    {
        IsDesignMode = false;
        var host = Host;
        base.OnStartup(e);
        host.StartAsync().ConfigureAwait(false);
    }

    protected override async void OnExit(ExitEventArgs e)
    {
        base.OnExit(e);

        var host = Host;
        host.StopAsync().ConfigureAwait(false);
        host.Dispose();
        _host = null;
    }
}