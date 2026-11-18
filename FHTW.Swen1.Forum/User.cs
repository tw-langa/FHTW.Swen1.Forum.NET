namespace FHTW.Swen1.Forum;



public sealed class User
{
    internal User()
    {}


    public required string UserName
    {
        get; set;
    }


    public required string FullName
    {
        get; set;
    }


    public required string EMail
    {
        get; set;
    }


    public void SetPassword(string password)
    {}


    public void Save()
    {}
}
