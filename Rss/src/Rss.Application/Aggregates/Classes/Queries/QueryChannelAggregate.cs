using Rss.Application.Aggregates.Interfaces.Queries;
using Rss.CDO.Enums.Chat;
using Rss.CDO.Enums.Response;
using Rss.CDO.Response;
using Rss.Domain.Entities;
using Rss.Persistence.Repos.Interfaces.Queries;
using System.Collections.Generic;

namespace Rss.Application.Aggregates.Classes.Queries
{
    public class QueryChannelAggregate : IQueryChannelAggregate
    {
        private readonly IQueryChannelRepo _queryChannelRepo;

        public QueryChannelAggregate(IQueryChannelRepo queryChannelRepo)
        {
            _queryChannelRepo = queryChannelRepo;
        }

        public Response<ChannelEntity> Get(int id)
        {
            return Response<ChannelEntity>.Create(_queryChannelRepo.Get(id), ResponseType.Success);
        }
        public Response<ChannelEntity> Get(long userId, long chatId)
        {
            return Response<ChannelEntity>.Create(_queryChannelRepo.Get(userId, chatId), ResponseType.Success);
        }
        public Response<ChannelEntity> Get(long userId, string name)
        {
            return Response<ChannelEntity>.Create(_queryChannelRepo.Get(userId, name), ResponseType.Success);
        }
        public Response<IList<ChannelEntity>> GetList()
        {
            return Response<IList<ChannelEntity>>.Create(_queryChannelRepo.GetList(), ResponseType.Success);
        }
        public Response<IList<ChannelEntity>> GetList(ChatType type)
        {
            return Response<IList<ChannelEntity>>.Create(_queryChannelRepo.GetList((int)type), ResponseType.Success);
        }
    }
}
