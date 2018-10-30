using Rss.Application.Aggregates.Interfaces.Commands;
using Rss.Application.Aggregates.Interfaces.Queries;
using Rss.Application.Services.Interfaces.Commands;
using Rss.CDO.Enums.Response;
using Rss.CDO.Response;
using Rss.Domain.Entities;

namespace Rss.Application.Services.Classes.Commands
{
    public class CommandRssChatRelationService : ICommandRssChatRelationService
    {
        private readonly ICommandRssChatRelationAggregate _commandRssChatAggregate = null;
        private readonly IQueryRssChatRelationAggregate _queryRssChatAggregate = null;
        private readonly IQueryChannelAggregate _queryChannelAggregate = null;

        public CommandRssChatRelationService(ICommandRssChatRelationAggregate commandRssChatAggregate,
            IQueryChannelAggregate queryChannelAggregate,
            IQueryRssChatRelationAggregate queryRssChatAggregate)
        {
            _commandRssChatAggregate = commandRssChatAggregate;
            _queryChannelAggregate = queryChannelAggregate;
            _queryRssChatAggregate = queryRssChatAggregate;
        }

        public Response<int> Add(RssChatRelationEntity input)
        {
            return _commandRssChatAggregate.Add(input);
        }
        public Response<int> Add(RssChatRelationEntity input, string name)
        {
            Response<ChannelEntity> response = _queryChannelAggregate.Get(input.UserId, name);
            input.ChatId = response.ResponseData.ChatId;
            input.Name = name;
            Response<RssChatRelationEntity> validation = _queryRssChatAggregate.Get(input.UserId, input.ChatId, input.Url);
            if (validation.ResponseData != null)
                return Response<int>.Create(0, CDO.Enums.Response.ResponseType.AlreadyExist);
            return _commandRssChatAggregate.Add(input);
        }
        public Response<int> Remove(RssChatRelationEntity input)
        {
            return _commandRssChatAggregate.Remove(input);
        }
        public Response<int> Remove(long userId, string name, string url)
        {
            Response<RssChatRelationEntity> response = _queryRssChatAggregate.Get(userId, name, url);
            if (response == null)
                return Response<int>.Create(0, ResponseType.NotFound);
            return Remove(response.ResponseData);
        }
        public Response<int> Update(RssChatRelationEntity input)
        {
            return _commandRssChatAggregate.Update(input);
        }
    }
}
