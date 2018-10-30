using Rss.Domain.Entities;
using Rss.Persistence.MySQL.Repos.Classes.Base.Command;
using Rss.Persistence.Repos.Interfaces.Commands;

namespace Rss.Persistence.MySQL.Repos.Classes.Commands
{
    public class CommandRssCatRelationRepo : CommandRepoBase<RssChatRelationEntity>, ICommandRssCatRelationRepo
    {
        private readonly string _connectionString;

        public CommandRssCatRelationRepo(string connectionString) : base(connectionString)
        {
            _connectionString = connectionString;
        }
    }
}
