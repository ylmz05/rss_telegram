using Rss.Domain.Entities;
using Rss.Persistence.MySQL.Repos.Classes.Base.Command;
using Rss.Persistence.Repos.Interfaces.Commands;

namespace Rss.Persistence.MySQL.Repos.Classes.Commands
{
    public class CommandBotRepo : CommandRepoBase<BotEntity>, ICommandBotRepo
    {
        private readonly string _connectionString;

        public CommandBotRepo(string connectionString) : base(connectionString)
        {
            _connectionString = connectionString;
        }
    }
}
