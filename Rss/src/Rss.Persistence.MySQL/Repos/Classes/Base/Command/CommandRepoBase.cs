using Dapper;
using DapperExtensions;
using MySql.Data.MySqlClient;
using System.Data;

namespace Rss.Persistence.MySQL.Repos.Classes.Base.Command
{
    public abstract class CommandRepoBase<T> where T : class
    {
        private readonly string _connectionString;

        public CommandRepoBase(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Add(T input)
        {
            using (IDbConnection connection = new MySqlConnection(_connectionString))
                connection.Insert<T>(input);
        }

        public void Remove(T input)
        {
            using (IDbConnection connection = new MySqlConnection(_connectionString))
                connection.Delete<T>(input);
        }

        public void Update(T input)
        {
            using (IDbConnection connection = new MySqlConnection(_connectionString))
                connection.Update<T>(input);
        }
    }
}
