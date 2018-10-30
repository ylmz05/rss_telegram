using Rss.Application.Aggregates.Interfaces.Commands;
using Rss.CDO.Enums.Response;
using Rss.CDO.Response;
using Rss.Domain.Entities;
using Rss.Persistence.Repos.Interfaces.Commands;

namespace Rss.Application.Aggregates.Classes.Commands
{
    public class CommandBotAggregate : ICommandBotAggregate
    {
        private readonly ICommandBotRepo _commandBotRepo;

        public CommandBotAggregate(ICommandBotRepo commandBotRepo)
        {
            _commandBotRepo = commandBotRepo;
        }

        public Response<int> Add(BotEntity input)
        {
            _commandBotRepo.Add(input);
            return Response<int>.Create(input.Id, ResponseType.Success);
        }
        public Response<int> Remove(BotEntity input)
        {
            _commandBotRepo.Remove(input);
            return Response<int>.Create(input.Id, ResponseType.Success);
        }
        public Response<int> Update(BotEntity input)
        {
            _commandBotRepo.Update(input);
            return Response<int>.Create(input.Id, ResponseType.Success);
        }
    }
}
