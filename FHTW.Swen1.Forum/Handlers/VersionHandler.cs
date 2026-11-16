using System.Net;
using System.Text.Json.Nodes;

using FHTW.Swen1.Forum.Server;

namespace FHTW.Swen1.Forum.Handlers;



internal class VersionHandler: Handler, IHandler
{
    public override bool Handle(HttpRestServerEventArgs e)
    {
        if((e.Method == HttpMethod.Get) && (e.Path == "/version"))
        {
            e.Respond(HttpStatusCode.OK, new() { ["version"] = "1.0" });
            return true;
        }

        return false;
    }
}
