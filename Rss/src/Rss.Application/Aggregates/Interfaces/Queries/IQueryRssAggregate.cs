using Rss.CDO.Response;
using Rss.Domain.Entities;

namespace Rss.Application.Aggregates.Interfaces.Queries
{
    public interface IQueryRssAggregate : IQueryAggregate<RssEntity>
    {
        Response<RssEntity> Get(long userId, string url);
    }
}
