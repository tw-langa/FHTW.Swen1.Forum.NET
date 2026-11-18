using System.Net;

using FHTW.Swen1.Forum.Server;

namespace FHTW.Swen1.Forum.Handlers;



public sealed class UserHandler: Handler, IHandler
{
    public override bool Handle(HttpRestServerEventArgs e)
    {
        if(!e.Path.StartsWith("/users")) {  return false; }

        if((e.Path == "/users") && (e.Method == HttpMethod.Post))
        {
            try
            {
                User user = new()
                {
                    UserName = e.Content?["username"]?.GetValue<string>() ?? string.Empty,
                    FullName = e.Content?["fullname"]?.GetValue<string>() ?? string.Empty,
                    EMail    = e.Content?["email"]?.GetValue<string>() ?? string.Empty
                };
                user.SetPassword(e.Content?["email"]?.GetValue<string>() ?? string.Empty);
                user.Save();

                e.Respond(HttpStatusCode.Created);

                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"[{nameof(UserHandler)}]: Handled ({e.Method.ToString()}) {e.Path}.");
            }
            catch(Exception ex)
            {
                e.Respond(HttpStatusCode.InternalServerError, new() { ["error"] = ex.Message });
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"[{nameof(UserHandler)}]: Exception while handling" +
                        $"({e.Method.ToString()}) {e.Path} - {ex.Message}");
            }
        }
        else
        {
            e.Respond(HttpStatusCode.BadRequest, new() { ["error"] = "Invalid endpoint." });

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"[{nameof(UserHandler)}]: Invalid endpoint ({e.Method.ToString()}) {e.Path}.");
        }

        return true;
    }
}
