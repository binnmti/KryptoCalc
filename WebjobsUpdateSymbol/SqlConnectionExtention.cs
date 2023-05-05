using Dapper;
using System.Data.SqlClient;
using System.Reflection;

namespace WebjobsUpdateSymbol;


public static class SqlConnectionExtention
{
    public static void Insert<T>(this SqlConnection sqlConnection, T data, string selectName, string selectValue)
    {
        var single = sqlConnection.Query<T>($"select * from [{typeof(T).Name}] Where {selectName} = '{selectValue}'").SingleOrDefault();
        if (single != null) return;

        var properties = data.GetType().GetProperties();
        var (sql, param) = (data.GetInsertSql(properties), data.GetParam(properties));
        sqlConnection.Execute(sql, param);
    }

    public static void Update<T>(this SqlConnection sqlConnection, T data)
    {
        var properties = data.GetType().GetProperties();
        var (sql, param) = (data.GetUpdateSql(properties), data.GetParam(properties));
        sqlConnection.Execute(sql, param);
    }

    private static DynamicParameters GetParam<T>(this T data, IEnumerable<PropertyInfo> propertyInfos)
        => new(propertyInfos.ToDictionary(p => $"@{p.Name}", p => p.GetValue(data)));

    private static string GetUpdateSql<T>(this T data, IEnumerable<PropertyInfo> infos)
    => $@"update {data?.GetType()?.Name} set {string.Join(",", infos.Select(x => $"{x.Name} = @{x.Name}"))} where {infos.First().Name} = @{infos.First().Name};";

    private static string GetInsertSql<T>(this T data, IEnumerable<PropertyInfo> infos)
        => $"insert into [{data?.GetType()?.Name}] ({string.Join(",", infos.Select(x => $"[{x.Name}]"))}) values ({string.Join(",", infos.Select(x => "@" + x.Name))});";
}
