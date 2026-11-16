namespace FHTW.Swen1.Forum;

using FHTW.Swen1.Forum.Handlers;
using FHTW.Swen1.Forum.Server;



internal class Program
{
    static void Main(string[] args)
    {
        HttpRestServer svr = new();
        svr.RequestReceived += Handler.HandleEvent;
        svr.Run();
    }
}
