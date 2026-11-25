namespace FHTW.Swen1.Forum.Repositories;

using System.Data;



public sealed class UserRepository: Repository<User>, IRepository<User>
{
    protected override string _TableName
    {
        get { return "USERS"; }
    }

    protected override string _Pk
    {
        get { return "USERNAME"; }
    }

    public override void Save(User obj, Session? session = null)
    {
        using IDbConnection cn = _GetConnection();
        using IDbCommand cmd = cn.CreateCommand($"INSERT INTO {_TableName} VALUES ({_Pk}, NAME, EMAIL, HADMIN) VALUES (:pk, :nam, :eml, :adm) "+
                                                $"ON CONFLICT({_Pk}) DO UPDATE SET NAME = :nam, EMAIL = :eml, HADMIN = :adm");
        cmd.WithParam(":pk", obj.UserName)
           .WithParam(":nam", obj.FullName)
           .WithParam(":eml", obj.EMail)
           .WithParam(":adm", obj.IsAdmin).ExecuteNonQuery();

    }

    protected override User _Refresh(User obj, IDataReader re)
    {
        obj.UserName = re.GetString(_Pk);
        obj.FullName = re.GetString("NAME");
        obj.EMail = re.GetString("EMAIL");
        obj.IsAdmin = re.GetBool("HADMIN");

        return obj;
    }
}
