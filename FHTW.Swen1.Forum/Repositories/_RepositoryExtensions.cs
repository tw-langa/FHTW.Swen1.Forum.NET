using System.Data;

namespace FHTW.Swen1.Forum.Repositories;


internal static class _RepositoryExtensions
{
    public static IDbCommand CreateCommand(this IDbConnection cn, string SQL)
    {
        IDbCommand cmd = cn.CreateCommand();
        cmd.CommandText = SQL;

        return cmd;
    }


    public static IDbCommand WithParam(this IDbCommand cmd, string name, object value)
    {
        IDataParameter p = cmd.CreateParameter();
        p.ParameterName = name;
        p.Value = value;
        cmd.Parameters.Add(p);

        return cmd;
    }


    public static string GetString(this IDataReader re, string name)
    {
        int idx = re.GetOrdinal(name);
        if(re.IsDBNull(idx)) { return string.Empty; }

        return re.GetString(idx);
    }


    public static bool GetBool(this IDataReader re, string name)
    {
        int idx = re.GetOrdinal(name);
        if(re.IsDBNull(idx)) { return false; }

        return re.GetBoolean(idx);
    }


    public static int GetInt(this IDataReader re, string name)
    {
        int idx = re.GetOrdinal(name);
        if(re.IsDBNull(idx)) { return 0; }

        return re.GetInt32(idx);
    }
}
