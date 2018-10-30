using Rss.Domain.Entities;
using Rss.Persistence.MySQL.Repos.Classes.Base.Query;
using Rss.Persistence.Repos.Interfaces.Queries;

namespace Rss.Persistence.MySQL.Repos.Classes.Queries
{
    public class QueryUserRepo : QueryRepoBase<UserEntity>, IQueryUserRepo
    {
        private readonly string _connectionString;

        public QueryUserRepo(string connectionString) : base(connectionString)
        {
            _connectionString = connectionString;
        }
    }
}
