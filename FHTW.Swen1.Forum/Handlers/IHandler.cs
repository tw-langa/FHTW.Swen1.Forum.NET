namespace FHTW.Swen1.Forum.Handlers;

using FHTW.Swen1.Forum.Server;



public interface IHandler
{
    public bool Handle(HttpRestServerEventArgs e);
}
