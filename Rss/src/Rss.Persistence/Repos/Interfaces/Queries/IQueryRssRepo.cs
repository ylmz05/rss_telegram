using Rss.Domain.Entities;
using System.Collections.Generic;

namespace Rss.Persistence.Repos.Interfaces.Queries
{
    public interface IQueryRssRepo : IQueryRepo<RssEntity>
    {
        RssEntity Get(long userId, string url);
        IList<RssEntity> GetList(long userId);
    }
}
