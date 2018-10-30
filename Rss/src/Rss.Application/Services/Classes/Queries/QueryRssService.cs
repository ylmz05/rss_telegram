using System.Collections.Generic;
using Rss.Application.Aggregates.Interfaces.Queries;
using Rss.Application.Services.Interfaces.Queries;
using Rss.CDO.Response;
using Rss.Domain.Entities;

namespace Rss.Application.Services.Classes.Queries
{
    public class QueryRssService : IQueryRssService
    {
        private readonly IQueryRssAggregate _queryRssAggregate = null;

        public QueryRssService(IQueryRssAggregate queryRssAggregate)
        {
            _queryRssAggregate = queryRssAggregate;
        }

        public Response<RssEntity> Get(int id)
        {
            return _queryRssAggregate.Get(id);
        }
        public Response<RssEntity> Get(long userId, string url)
        {
            return _queryRssAggregate.Get(userId, url);
        }
        public Response<IList<RssEntity>> GetList()
        {
            return _queryRssAggregate.GetList();
        }
    }
}
