using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;

namespace CV19;

class Program
{
    [STAThread]
    public static void Main()
    {
        var app = new CV19.App();
        app.InitializeComponent();
        app.Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] Args)
    {
        var hostBuilder = Host.CreateDefaultBuilder(Args);
        hostBuilder.UseContentRoot(Environment.CurrentDirectory);
        hostBuilder.ConfigureAppConfiguration((host, cfg) =>
        {
            cfg.SetBasePath(Environment.CurrentDirectory);
            cfg.AddJsonFile("appsettings.json", true, true);
        });

        hostBuilder.ConfigureServices(App.ConfigureServices);

        return hostBuilder;
    }
}
