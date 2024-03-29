﻿using System.Net;

namespace CV19.Web;

public class ContextReceiverEventArgs : EventArgs 
{
    public HttpListenerContext Context { get; }

    public ContextReceiverEventArgs(HttpListenerContext context) => Context = context;
    
}

public class WebServer
{
    //private TcpListener _listener = new TcpListener(new IPEndPoint(IPAddress.Any, 8080));
    private HttpListener _listener;
    private readonly int _port;
    private bool _enabled;
    private readonly object _lock = new object();
    


    public int Port => _port;
    public bool Enabled
    {
        get => _enabled;
        set
        {
            if(value) Start();
            else Stop();
        }
    }

    public WebServer(int port) => _port = port;
    public event EventHandler<ContextReceiverEventArgs> RequestReceived;

    public void Start()
    {
        if (_enabled) return;
        lock(_lock) 
        {
            if (_enabled) return;

            _listener = new HttpListener();
            _listener.Prefixes.Add($"http://*:{_port}/"); // netsh http add user urlacl url=http://*:8080/ user=user_name
            _listener.Prefixes.Add($"http://+:{_port}/"); // netsh http add user urlacl url=http://+:8080/ user=user_name
            _enabled = true;
        }
        ListenAsync();
    }

    public void Stop() 
    {
        if(!_enabled) return;

        lock(_lock) 
        {
            if (!_enabled) return;

            _listener = null;
            _enabled = false;
        }
    }

    private async void ListenAsync()
    {
        var listener = _listener;
        listener.Start();

        HttpListenerContext context = null;
        while(_enabled) 
        {
            var get_context_task = listener.GetContextAsync();
            if(context != null)
                ProcessRequestAsync(context);
            context = await get_context_task.ConfigureAwait(false);
        }

        listener.Stop();
    }

    private async void ProcessRequestAsync(HttpListenerContext context)
    {
        await Task.Run(() => RequestReceived?.Invoke(this, new ContextReceiverEventArgs(context)));
    }

}