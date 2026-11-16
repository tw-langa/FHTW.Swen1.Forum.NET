namespace FHTW.Swen1.Forum.Server;

using System.Net;
using System.Text;
using System.Text.Json.Nodes;



public class HttpRestServerEventArgs: EventArgs
{
    public HttpRestServerEventArgs(HttpListenerContext context)
    {
        Context = context;

        Method = HttpMethod.Parse(context.Request.HttpMethod);
        Path = context.Request.Url?.AbsolutePath ?? string.Empty;

        Console.ForegroundColor = ConsoleColor.Gray;
        Console.WriteLine($"Received: {Method} {Path}");

        if(context.Request.HasEntityBody)
        {
            using Stream input = context.Request.InputStream;
            using StreamReader re = new(input, context.Request.ContentEncoding);
            Body = re.ReadToEnd();
            Content = JsonNode.Parse(Body)?.AsObject() ?? new();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(Body);
        }
        else
        {
            Body = string.Empty;
            Content = new();
        }
    }


    public HttpListenerContext Context { get; }

    public HttpMethod Method { get; }

    public string Path { get; }

    public string Body { get; }

    public JsonObject Content { get; }

    public bool Responded
    {
        get; set;
    } = false;


    public void Respond(HttpStatusCode statusCode, JsonObject? content = null)
    {
        Respond((int) statusCode, content);
    }

    public void Respond(int statusCode, JsonObject? content = null)
    {
        HttpListenerResponse response = Context.Response;
        response.StatusCode = statusCode;
        string rstr = content?.ToString() ?? string.Empty;

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"Responding: {statusCode}: {rstr}");

        byte[] buffer = Encoding.UTF8.GetBytes(rstr);
        response.ContentLength64 = buffer.Length;
        response.ContentType = "application/json";

        using Stream output = response.OutputStream;
        output.Write(buffer, 0, buffer.Length);
        
        Responded = true;
    }
}
