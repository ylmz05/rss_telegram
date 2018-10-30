using Rss.Application.Aggregates.Interfaces.Commands;
using Rss.Application.Services.Interfaces.Commands;
using Rss.CDO.Response;
using Rss.Domain.Entities;

namespace Rss.Application.Services.Classes.Commands
{
    public class CommandBotService : ICommandBotService
    {
        private readonly ICommandBotAggregate _commandBotAggregate = null;

        public CommandBotService(ICommandBotAggregate commandBotAggregate)
        {
            _commandBotAggregate = commandBotAggregate;
        }

        public Response<int> Add(BotEntity input)
        {
            return _commandBotAggregate.Add(input);
        }
        public Response<int> Remove(BotEntity input)
        {
            return _commandBotAggregate.Remove(input);
        }
        public Response<int> Update(BotEntity input)
        {
            return _commandBotAggregate.Update(input);
        }
    }
}
