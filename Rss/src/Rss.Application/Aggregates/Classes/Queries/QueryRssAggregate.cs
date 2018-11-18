using Rss.Application.Aggregates.Interfaces.Queries;
using Rss.CDO.Enums.Response;
using Rss.CDO.Response;
using Rss.Domain.Entities;
using Rss.Persistence.Repos.Interfaces.Queries;
using System.Collections.Generic;

namespace Rss.Application.Aggregates.Classes.Queries
{
    public class QueryRssAggregate : IQueryRssAggregate
    {
        private readonly IQueryRssRepo _queryRssRepo;

        public QueryRssAggregate(IQueryRssRepo queryRssRepo)
        {
            _queryRssRepo = queryRssRepo;
        }

        public Response<RssEntity> Get(int id)
        {
            return Response<RssEntity>.Create(_queryRssRepo.Get(id), ResponseType.Success);
        }
        public Response<RssEntity> Get(long userId, string aliasName)
        {
            return Response<RssEntity>.Create(_queryRssRepo.Get(userId, aliasName), ResponseType.Success);
        }
        public Response<IList<RssEntity>> GetList(long userId)
        {
            return Response<IList<RssEntity>>.Create(_queryRssRepo.GetList(userId), ResponseType.Success);
        }

        public Response<IList<RssEntity>> GetList()
        {
            return Response<IList<RssEntity>>.Create(_queryRssRepo.GetList(), ResponseType.Success);
        }
    }
}
