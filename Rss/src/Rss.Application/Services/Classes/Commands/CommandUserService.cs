using Rss.Application.Aggregates.Interfaces.Commands;
using Rss.Application.Services.Interfaces.Commands;
using Rss.CDO.Response;
using Rss.Domain.Entities;

namespace Rss.Application.Services.Classes.Commands
{
    public class CommandUserService : ICommandUserService
    {
        private readonly ICommandUserAggregate _commandUserAggregate = null;

        public CommandUserService(ICommandUserAggregate commandUserAggregate)
        {
            _commandUserAggregate = commandUserAggregate;
        }

        public Response<int> Add(UserEntity input)
        {
            return _commandUserAggregate.Add(input);
        }
        public Response<int> Remove(UserEntity input)
        {
            return _commandUserAggregate.Remove(input);
        }
        public Response<int> Update(UserEntity input)
        {
            return _commandUserAggregate.Update(input);
        }
    }
}
