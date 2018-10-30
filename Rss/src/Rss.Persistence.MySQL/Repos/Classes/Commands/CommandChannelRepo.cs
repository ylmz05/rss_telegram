using Rss.Domain.Entities;
using Rss.Persistence.MySQL.Repos.Classes.Base.Command;
using Rss.Persistence.Repos.Interfaces.Commands;

namespace Rss.Persistence.MySQL.Repos.Classes.Commands
{
    public class CommandChannelRepo : CommandRepoBase<ChannelEntity>, ICommandChannelRepo
    {
        private readonly string _connectionString;

        public CommandChannelRepo(string connectionString) : base(connectionString)
        {
            _connectionString = connectionString;
        }
    }
}
