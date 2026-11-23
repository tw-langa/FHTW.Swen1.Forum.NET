namespace FHTW.Swen1.Forum;

using FHTW.Swen1.Forum.Repositories;

using System.Security;




public abstract class Atom<T>: IAtom, _IAtom where T: class, IAtom, new()
{
    protected abstract object _ObjectID { get; set; }

    protected Session? _EditingSession = null;

    protected IRepository<T> _Repository = Repository<T>.For<T>();



    public virtual void BeginEdit(Session session)
    {
        if(session is null) { throw new ArgumentNullException(nameof(session)); }
        if(!session.Valid)  { throw new SecurityException("Session is not valid."); }
        
        _EditingSession = session;
    }


    public virtual void Delete()
    {
        _Repository.Delete((T)(object) this, _EditingSession);
    }


    public void Refresh()
    {
        _Repository.Refresh((T)(object) this, _EditingSession);
    }


    public void Save()
    {
        _Repository.Save((T)(object) this, _EditingSession);
    }



    object _IAtom.ObjectID
    {
        get { return _ObjectID; }
        set { _ObjectID = value; }
    }
}
