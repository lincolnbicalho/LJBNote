using System.Data;

namespace Infrastructure.DataAccess
{
    public interface ISqlDataAccess
    {
        Task<List<T>> LoadData<T, U>(string sqlQuery, U parameters, CommandType commandType);
        Task<List<T>> LoadData<T, U, TInner>(string sqlQuery, U parameters, CommandType commandType, Func<T, TInner, T> map);
        Task SaveData<T>(string sqlQuery, T parameters, CommandType commandType);
    }
}