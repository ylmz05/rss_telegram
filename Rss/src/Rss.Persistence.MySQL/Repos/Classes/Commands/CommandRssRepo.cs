using Rss.Domain.Entities;
using Rss.Persistence.MySQL.Repos.Classes.Base.Command;
using Rss.Persistence.Repos.Interfaces.Commands;

namespace Rss.Persistence.MySQL.Repos.Classes.Commands
{
    public class CommandRssRepo : CommandRepoBase<RssEntity>, ICommandRssRepo
    {
        private readonly string _connectionString;

        public CommandRssRepo(string connectionString) : base(connectionString)
        {
            _connectionString = connectionString;
        }
    }
}
