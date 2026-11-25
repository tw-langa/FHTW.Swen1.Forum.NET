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


    public static IRepository<Z> For<Z>() where Z: class, IAtom, new()
    {
        return null!;               // TODO: get specific repositories.
    }


    protected virtual string _Pk
    {
        get { return "ID"; }
    }

    protected abstract string _TableName { get; }

    protected abstract T _Refresh(T obj, IDataReader re);


    public virtual T? Get(object id, Session? session = null)
    {
        using IDbConnection cn = _GetConnection();
        using IDbCommand cmd = cn.CreateCommand($"SELECT * FROM {_TableName} WHERE {_Pk} = :pk");
        using IDataReader re = cmd.WithParam(":pk", id).ExecuteReader();

        if(re.Read())
        {
            return _Refresh(new T(), re);
        }
        return null;
    }

    public virtual IEnumerable<T> GetAll(Session? session = null)
    {
        using IDbConnection cn = _GetConnection();
        using IDbCommand cmd = cn.CreateCommand($"SELECT * FROM {_TableName}");
        using IDataReader re = cmd.ExecuteReader();

        List<T> rval = new();
        while(re.Read())
        {
            rval.Add(_Refresh(new T(), re));
        }
        return rval;
    }

    public virtual void Refresh(T obj, Session? session = null)
    {
        using IDbConnection cn = _GetConnection();
        using IDbCommand cmd = cn.CreateCommand($"SELECT * FROM {_TableName} WHERE {_Pk} = :pk");
        using IDataReader re = cmd.WithParam(":pk", ((_IAtom) obj).ObjectID).ExecuteReader();
                
        if(re.Read()) { _Refresh(obj, re);  }
    }

    public abstract void Save(T obj, Session? session = null);

    public virtual void Delete(T obj, Session? session = null)
    {
        using IDbConnection cn = _GetConnection();
        using IDbCommand cmd = cn.CreateCommand($"DELETE FROM {_TableName} WHERE {_Pk} = :pk");
        cmd.WithParam(":pk", ((_IAtom) obj).ObjectID).ExecuteNonQuery();
    }
}
