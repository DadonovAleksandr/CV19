using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CV19.Web;

namespace CV19Console;

internal class WebServerTest
{
    public static void Run()
    {
        var server = new WebServer(8080);
        server.Start();
        server.RequestReceived += OnRequestReceived;

        Console.WriteLine("Сервер запущен!");
        Console.ReadLine();
    }

    private static void OnRequestReceived(object? sender, ContextReceiverEventArgs e)
    {
        var context = e.Context;

        Console.WriteLine($"Connection: {context.Request.UserHostAddress}");

        using var writer = new StreamWriter(context.Response.OutputStream);
        writer.WriteLine("Hello from Test Web Server!!!");
        
    }

}

