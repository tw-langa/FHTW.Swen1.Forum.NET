namespace FHTW.Swen1.Forum;



public sealed class User: Atom<User>, IAtom, _IAtom, _IAuthenticable
{
    public User()
    {}


    protected override object _ObjectID
    {
        get { return UserName; }
        set { UserName = (string) value; }
    }


    public string UserName
    {
        get; set;
    } = string.Empty;


    public string FullName
    {
        get; set;
    } = string.Empty;


    public string EMail
    {
        get; set;
    } = string.Empty;


    public bool IsAdmin
    {
        get; set;
    } = false;


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
