namespace FHTW.Swen1.Forum.Repositories;

using System.Data;
using System.Data.SQLite;



public abstract class Repository<T>: IRepository<T> where T: class, IAtom, new()
{
    protected static IDbConnection _GetConnection()
    {
        SQLiteConnection rval = new("Data Source=forum.db;Version=3;");
        rval.Open();

        return rval;
    }


    protected virtual T _CreateObject(IDataReader re)
    {
        T rval = new();
        ((_IAtom) rval).ObjectID = re.GetInt32(0);
        
        return _RefreshObject(rval, re);
    }


    public static IRepository<Z> For<Z>() where Z: class, IAtom, new()
    {
        return null!;               // TODO: get specific repositories.
    }


    protected abstract T _RefreshObject(T obj, IDataReader re);


    public abstract T? Get(object id, Session? session = null);

    public abstract IEnumerable<T> GetAll(Session? session = null);

    public abstract void Refresh(T obj, Session? session = null);

    public abstract void Save(T obj, Session? session = null);

    public abstract void Delete(T obj, Session? session = null);
}
