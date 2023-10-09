using Dapper;
using System.Data.SqlClient;
using System.Reflection;

namespace WebjobsUpdateSymbol;

public static class SqlConnectionExtention
{

    public static void InsertOrUpdate<T>(this SqlConnection sqlConnection, T data, string value)
    {
        if (data == null) return;

        var single = sqlConnection.Query<T>($"select * from [{typeof(T).Name}] Where Id = '{value}'").SingleOrDefault();
        var properties = data.GetType().GetProperties();
        var (sql, param) = (single != null ? data.GetUpdateSql(properties) : data.GetInsertSql(properties), data.GetParam(properties));
        sqlConnection.Execute(sql, param);
    }

    public static void Insert<T>(this SqlConnection sqlConnection, T data)
    {
        //TODO:Idがintの場合はSkipするみたいな感じがベター
        IEnumerable<PropertyInfo> properties;
        if (data?.GetType()?.GetProperty("Id")?.PropertyType == typeof(int))
        {
            properties = data.GetType().GetProperties().Skip(1);
        }
        else
        {
            properties = data?.GetType().GetProperties().AsEnumerable() ?? new List<PropertyInfo>();
        }
        var (sql, param) = (data.GetInsertSql(properties), data.GetParam(properties));
        sqlConnection.Execute(sql, param);
    }

    public static void Update<T>(this SqlConnection sqlConnection, T data)
    {
        if (data == null) return;

        var properties = data.GetType().GetProperties();
        var (sql, param) = (data.GetUpdateSql(properties), data.GetParam(properties));
        sqlConnection.Execute(sql, param);
    }

    private static string GetUpdateSql<T>(this T data, IEnumerable<PropertyInfo> infos)
        => $@"update {data?.GetType()?.Name} set {string.Join(",", infos.Select(x => $"{x.Name} = @{x.Name}"))} where {infos.First().Name} = @{infos.First().Name};";

    private static string GetInsertSql<T>(this T data, IEnumerable<PropertyInfo> infos)
        => $"insert into [{data?.GetType()?.Name}] ({string.Join(",", infos.Select(x => $"[{x.Name}]"))}) values ({string.Join(",", infos.Select(x => "@" + x.Name))});";

    private static DynamicParameters GetParam<T>(this T data, IEnumerable<PropertyInfo> infos)
        => new(infos.ToDictionary(p => $"@{p.Name}", p => p.GetValue(data)));
}
