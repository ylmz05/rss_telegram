using Rss.Application.Aggregates.Interfaces.Commands;
using Rss.CDO.Enums.Response;
using Rss.CDO.Response;
using Rss.Domain.Entities;
using Rss.Persistence.Repos.Interfaces.Commands;

namespace Rss.Application.Aggregates.Classes.Commands
{
    public class CommandChannelAggregate : ICommandChannelAggregate
    {
        private readonly ICommandChannelRepo _commandChannelRepo;

        public CommandChannelAggregate(ICommandChannelRepo commandChannelRepo)
        {
            _commandChannelRepo = commandChannelRepo;
        }

        public Response<int> Add(ChannelEntity input)
        {
            _commandChannelRepo.Add(input);
            return Response<int>.Create(input.Id, ResponseType.Success);
        }
        public Response<int> Remove(ChannelEntity input)
        {
            _commandChannelRepo.Remove(input);
            return Response<int>.Create(input.Id, ResponseType.Success);
        }
        public Response<int> Update(ChannelEntity input)
        {
            _commandChannelRepo.Update(input);
            return Response<int>.Create(input.Id, ResponseType.Success);
        }
    }
}
