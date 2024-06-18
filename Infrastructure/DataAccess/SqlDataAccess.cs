using System.Data;
using System.Data.SqlClient;
using System.Reflection.Metadata;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.DataAccess;

public class SqlDataAccess : ISqlDataAccess
{
    private readonly IConfiguration _configuration;

    public SqlDataAccess(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<List<T>> LoadData<T, U>(string sqlQuery, U parameters, CommandType commandType)
    {
        string connectionString = _configuration.GetConnectionString("Default");

        using IDbConnection connection = new SqlConnection(connectionString);

        var rows = await connection.QueryAsync<T>(sqlQuery, parameters, commandType: commandType);

        return rows.ToList();
    }

    /// <summary>
    /// This function if for nested objects
    /// </summary>
    /// <example>
    /// Func<Category, CategoryDetails, Category> map = (category, details) =>
    ///{
    ///    category.Details = details;
    ///    return category;
    ///};
    /// </example>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="U"></typeparam>
    /// <typeparam name="TInner"></typeparam>
    /// <param name="sqlQuery"></param>
    /// <param name="parameters"></param>
    /// <param name="commandType"></param>
    /// <param name="map"></param>
    /// <returns></returns>
    public async Task<List<T>> LoadData<T, U, TInner>(string sqlQuery, U parameters, CommandType commandType, Func<T, TInner, T> map)
    {
        string connectionString = _configuration.GetConnectionString("Default");

        using IDbConnection connection = new SqlConnection(connectionString);

        var rows = await connection.QueryAsync<T, TInner, T>(
            sqlQuery,
            map,
            parameters,
            commandType: commandType
        );

        return rows.ToList();
    }


    public async Task SaveData<T>(string sqlQuery, T parameters, CommandType commandType)
    {
        string connectionString = _configuration.GetConnectionString("Default");

        using IDbConnection connection = new SqlConnection(connectionString);

        await connection.ExecuteAsync(sqlQuery, parameters, commandType: commandType);
    }
}
