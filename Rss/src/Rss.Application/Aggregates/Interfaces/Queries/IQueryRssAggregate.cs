using Rss.CDO.Response;
using Rss.Domain.Entities;
using System.Collections.Generic;

namespace Rss.Application.Aggregates.Interfaces.Queries
{
    public interface IQueryRssAggregate : IQueryAggregate<RssEntity>
    {
        Response<RssEntity> Get(long userId, string aliasName);
        Response<IList<RssEntity>> GetList(long userId);
    }
}
