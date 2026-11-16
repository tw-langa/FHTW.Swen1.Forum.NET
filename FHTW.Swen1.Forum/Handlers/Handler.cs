using System.Reflection;

using FHTW.Swen1.Forum.Server;

namespace FHTW.Swen1.Forum.Handlers;


public abstract class Handler: IHandler
{
    private static List<IHandler> _Handlers = _GetHandlers();

    private static List<IHandler> _GetHandlers()
    {
        List<IHandler> rval = new();

        foreach(Type i in Assembly.GetExecutingAssembly().GetTypes()
               .Where(m => m.IsAssignableTo(typeof(IHandler)) && !m.IsAbstract))
        {
            try
            {
                IHandler? h = (IHandler?) Activator.CreateInstance(i);
                if(h is not null) { rval.Add(h); }
            }
            catch(Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Failed to create instance of {i.Name}.");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine(ex.ToString());
            }
        }

        return rval;
    }


    public static void HandleEvent(object? sender, HttpRestServerEventArgs e)
    {
        foreach(IHandler i in _Handlers)
        {
            i.Handle(e);
            if(e.Responded) { break; }
        }
    }

    public abstract bool Handle(HttpRestServerEventArgs e);
}
