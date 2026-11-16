namespace FHTW.Swen1.Forum.Server;

using System;
using System.Net;
using System.Threading.Tasks;



public sealed class HttpRestServer
{
    private readonly HttpListener _Listener;

    public event EventHandler<HttpRestServerEventArgs>? RequestReceived;


    public HttpRestServer(int port = 12000)
    {
        _Listener = new();
        _Listener.Prefixes.Add($"http://+:{port}/");
    }


    public bool Running
    {
        get; private set;
    }


    public void Stop()
    {
        _Listener.Close();
        Running = false;
    }


    public void Run()
    {
        _Listener.Start();
        Running = true;

        while(Running)
        {
            HttpListenerContext context = _Listener.GetContext();

            _ = Task.Run(() =>
            {
                HttpRestServerEventArgs args = new(context);
                RequestReceived?.Invoke(this, args);

                if(!args.Responded)
                {
                    args.Respond(404);
                }
            }).ConfigureAwait(true);
        }
    }
}
