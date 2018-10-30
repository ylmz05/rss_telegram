using Rss.Domain.Entities;
using Rss.Persistence.MySQL.Repos.Classes.Base.Query;
using Rss.Persistence.Repos.Interfaces.Queries;

namespace Rss.Persistence.MySQL.Repos.Classes.Queries
{
    public class QueryBotRepo : QueryRepoBase<BotEntity>, IQueryBotRepo
    {
        private readonly string _connectionString;

        public QueryBotRepo(string connectionString) : base(connectionString)
        {
            _connectionString = connectionString;
        }
    }
}
