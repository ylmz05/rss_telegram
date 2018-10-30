using DapperExtensions;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Rss.Persistence.MySQL.Repos.Classes.Base.Query
{
    public abstract class QueryRepoBase<T> where T : class
    {
        private readonly string _connectionString;

        public QueryRepoBase(string connectionString)
        {
            _connectionString = connectionString;
        }
        public T Get(int id)
        {
            T output = null;

            using (IDbConnection connection = new MySqlConnection(_connectionString))
                output = connection.Get<T>(id);

            return output;
        }

        public IList<T> GetList()
        {
            IList<T> output = null;

            using (IDbConnection connection = new MySqlConnection(_connectionString))
                output = connection.GetList<T>().ToList();

            return output;
        }
    }
}
