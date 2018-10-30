using Rss.Domain.Entities;

namespace Rss.Persistence.Repos.Interfaces.Queries
{
    public interface IQueryRssRepo : IQueryRepo<RssEntity>
    {
        RssEntity Get(long userId, string url);
    }
}
