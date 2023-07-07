using CV19.Services.Interfaces;
using CV19.Web;
using System;
using System.IO;

namespace CV19.Services;

internal class HttpListenerWebService : IWebServerService
{
    private WebServer _server = new WebServer(8080); 
    public bool Enabled 
    { 
        get => _server.Enabled; 
        set => _server.Enabled = value; 
    }

    public HttpListenerWebService()
    {
        _server.RequestReceived += OnRequestReceived;
    }

    private void OnRequestReceived(object? sender, ContextReceiverEventArgs e)
    {
        using var writer = new StreamWriter(e.Context.Response.OutputStream);
        writer.WriteLine("CV-19 Application");
    }


    public void Start() => _server.Start();
    
    public void Stop() => _server.Stop();
    
}
