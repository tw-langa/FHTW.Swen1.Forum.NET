using System.Net;

using FHTW.Swen1.Forum.Server;

namespace FHTW.Swen1.Forum.Handlers;


public sealed class SessionHandler: Handler, IHandler
{
    public override bool Handle(HttpRestServerEventArgs e)
    {
        if(!e.Path.StartsWith("/sessions")) { return false; }

        if((e.Path == "/sessions") && (e.Method == HttpMethod.Post))
        {
            try
            {
                Session session = Session.Create(e.Content?["username"]?.GetValue<string>() ?? string.Empty,
                                                 e.Content?["password"]?.GetValue<string>() ?? string.Empty);
                e.Respond(session.Valid ? HttpStatusCode.Created : HttpStatusCode.Unauthorized);

                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"[{nameof(SessionHandler)}]: Handled ({e.Method.ToString()}) {e.Path}.");
            }
            catch(Exception ex)
            {
                e.Respond(HttpStatusCode.InternalServerError, new() { ["error"] = ex.Message });
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"[{nameof(SessionHandler)}]: Exception while handling" +
                        $"({e.Method.ToString()}) {e.Path} - {ex.Message}");
            }
        }
        else
        {
            e.Respond(HttpStatusCode.BadRequest, new() { ["error"] = "Invalid endpoint." });

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"[{nameof(SessionHandler)}]: Invalid endpoint ({e.Method.ToString()}) {e.Path}.");
        }

        return true;
    }
}
