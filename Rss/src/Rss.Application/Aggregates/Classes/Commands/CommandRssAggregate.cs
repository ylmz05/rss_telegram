using Rss.Application.Aggregates.Interfaces.Commands;
using Rss.CDO.Enums.Response;
using Rss.CDO.Response;
using Rss.Domain.Entities;
using Rss.Persistence.Repos.Interfaces.Commands;

namespace Rss.Application.Aggregates.Classes.Commands
{
    public class CommandRssAggregate : ICommandRssAggregate
    {
        private readonly ICommandRssRepo _commandRssRepo;

        public CommandRssAggregate(ICommandRssRepo commandRssRepo)
        {
            _commandRssRepo = commandRssRepo;
        }

        public Response<int> Add(RssEntity input)
        {
            _commandRssRepo.Add(input);
            return Response<int>.Create(input.Id, ResponseType.Success);
        }
        public Response<int> Remove(RssEntity input)
        {
            _commandRssRepo.Remove(input);
            return Response<int>.Create(input.Id, ResponseType.Success);
        }
        public Response<int> Update(RssEntity input)
        {
            _commandRssRepo.Update(input);
            return Response<int>.Create(input.Id, ResponseType.Success);
        }
    }
}
