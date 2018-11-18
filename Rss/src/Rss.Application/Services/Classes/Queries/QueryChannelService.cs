using System.Collections.Generic;
using Rss.Application.Aggregates.Interfaces.Queries;
using Rss.Application.Services.Interfaces.Queries;
using Rss.CDO.Enums.Chat;
using Rss.CDO.Response;
using Rss.Domain.Entities;

namespace Rss.Application.Services.Classes.Queries
{
    public class QueryChannelService : IQueryChannelService
    {
        private readonly IQueryChannelAggregate _queryChannelAggregate = null;

        public QueryChannelService(IQueryChannelAggregate queryChannelAggregate)
        {
            _queryChannelAggregate = queryChannelAggregate;
        }

        public Response<ChannelEntity> Get(int id)
        {
            return _queryChannelAggregate.Get(id);
        }
        public Response<IList<ChannelEntity>> GetList()
        {
            return _queryChannelAggregate.GetList();
        }
        public Response<IList<ChannelEntity>> GetList(long userId)
        {
            return _queryChannelAggregate.GetList(userId);
        }
        public Response<IList<ChannelEntity>> GetList(long userId, ChatType type)
        {
            return _queryChannelAggregate.GetList(userId, type);
        }
        public Response<IList<ChannelEntity>> GetList(ChatType type)
        {
            return _queryChannelAggregate.GetList(type);
        }
    }
}
