using Rss.Application.Services.Classes;
using Rss.CDO.Response;
using Rss.Domain.Entities;

namespace Rss.Application.Services.Interfaces.Queries
{
    public interface IQueryRssService : IQueryService<RssEntity>
    {
        Response<RssEntity> Get(long userId, string url);
    }
}
