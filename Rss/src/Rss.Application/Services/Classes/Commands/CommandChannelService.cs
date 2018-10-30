using Rss.Application.Aggregates.Interfaces.Commands;
using Rss.Application.Aggregates.Interfaces.Queries;
using Rss.Application.Services.Interfaces.Commands;
using Rss.CDO.Response;
using Rss.Domain.Entities;

namespace Rss.Application.Services.Classes.Commands
{
    public class CommandChannelService : ICommandChannelService
    {
        private readonly ICommandChannelAggregate _commandChannelAggregate = null;
        private readonly IQueryChannelAggregate _queryChannelAggregate = null;
        public CommandChannelService(ICommandChannelAggregate commandChannelAggregate, 
            IQueryChannelAggregate queryChannelAggregate)
        {
            _commandChannelAggregate = commandChannelAggregate;
            _queryChannelAggregate = queryChannelAggregate;
        }

        public Response<int> Add(ChannelEntity input)
        {
            Response<ChannelEntity> validation = _queryChannelAggregate.Get(input.UserId, input.ChatId);
            if (validation.ResponseData != null)
                return Response<int>.Create(0, CDO.Enums.Response.ResponseType.AlreadyExist);
            return _commandChannelAggregate.Add(input);
        }
        public Response<int> Remove(ChannelEntity input)
        {
            Response<ChannelEntity> validation = _queryChannelAggregate.Get(input.UserId, input.Name);
            if (validation.ResponseData != null)
                return _commandChannelAggregate.Remove(validation.ResponseData);
            else return Response<int>.Create(0, CDO.Enums.Response.ResponseType.NotFound);
        }
        public Response<int> Update(ChannelEntity input)
        {
            return _commandChannelAggregate.Update(input);
        }
    }
}
