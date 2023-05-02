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

    public static IHostBuilder CreateHostBuilder(string[] Args) => Host.CreateDefaultBuilder(Args)
        .UseContentRoot(App.CurrentDirectory)
        .ConfigureAppConfiguration((host, cfg) => cfg
            .SetBasePath(App.CurrentDirectory)
            .AddJsonFile("appsettings.json", true, true))
        .ConfigureServices(App.ConfigureServices);

}
