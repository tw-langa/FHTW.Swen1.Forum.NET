namespace FHTW.Swen1.Forum;



public sealed class Session
{
    private const string _ALPHABET = "1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";


    private static readonly Dictionary<string, Session> _Sessions = new();


    private Session(string userName, string password)
    {
        UserName = userName;
        IsAdmin = (userName == "admin");

        Token = string.Empty;
        Random rnd = new();
        for(int i = 0; i < 24; i++) { Token += _ALPHABET[rnd.Next(0, 62)]; }

        lock(_Sessions) { _Sessions.Add(Token, this); }
    }


    public static Session Create(string userName, string password)
    {
        return new Session(userName, password);
    }


    public static Session? Get(string token)
    {
        lock(_Sessions)
        {
            if(_Sessions.ContainsKey(token)) { return _Sessions[token]; }
        }

        return null;
    }

    public string Token
    {
        get;
    }

    public string? UserName
    {
        get;
    }

    public bool Valid
    {
        get
        {
            lock(_Sessions) { return _Sessions.ContainsKey(Token); }
        }
    }


    public bool IsAdmin
    {
        get;
    }


    public void Close()
    {
        lock(_Sessions)
        {
            if(_Sessions.ContainsKey(Token)) { _Sessions.Remove(Token); }
        }
    }
}
