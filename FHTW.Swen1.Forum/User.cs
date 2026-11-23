namespace FHTW.Swen1.Forum;



public sealed class User: Atom<User>, IAtom, _IAtom, _IAuthenticable
{
    internal User()
    {}


    protected override object _ObjectID
    {
        get { return UserName; }
        set { UserName = (string) value; }
    }


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
    {
        // TODO: Implement password hashing and salting
    }


    public string PasswordHash
    {
        get { throw new NotImplementedException(); }        // TODO: Implement password hashing and storage
        set { throw new NotImplementedException(); }
    }
}
