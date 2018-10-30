using Rss.Domain.Entities;
using Rss.Persistence.MySQL.Repos.Classes.Base.Command;
using Rss.Persistence.Repos.Interfaces.Commands;

namespace Rss.Persistence.MySQL.Repos.Classes.Commands
{
    public class CommandUserRepo : CommandRepoBase<UserEntity>, ICommandUserRepo
    {
        private readonly string _connectionString;

        public CommandUserRepo(string connectionString) : base(connectionString)
        {
            _connectionString = connectionString;
        }
    }
}
