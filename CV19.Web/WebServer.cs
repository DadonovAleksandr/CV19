using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace CV19.Web;

internal class ContextReceiverEventArgs : EventArgs 
{
    public HttpListenerContext Context { get; }

    public ContextReceiverEventArgs(HttpListenerContext context) => Context = context;
    
}

internal class WebServer
{
    //private TcpListener _listener = new TcpListener(new IPEndPoint(IPAddress.Any, 8080));
    private HttpListener _listener;
    private readonly int _port;
    private bool _enabled;
    private readonly object _lock = new object();
    private event EventHandler<ContextReceiverEventArgs> RequestReceived;


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
    

    public void Start()
    {
        if (_enabled) return;
        lock(_lock) 
        {
            if (_enabled) return;

            _listener = new HttpListener();
            _listener.Prefixes.Add($"http://*:{_port}");
            _listener.Prefixes.Add($"http://+:{_port}");
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


        while(_enabled) 
        {
            var context = await listener.GetContextAsync().ConfigureAwait(false);
            ProcessRequest(context);
            
        }

        listener.Stop();
    }

    private void ProcessRequest(HttpListenerContext context)
    {
        RequestReceived?.Invoke(this, new ContextReceiverEventArgs(context));
    }


}