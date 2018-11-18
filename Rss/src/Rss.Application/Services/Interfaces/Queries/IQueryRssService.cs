using Rss.Application.Services.Classes;
using Rss.CDO.Response;
using Rss.Domain.Entities;
using System.Collections.Generic;

namespace Rss.Application.Services.Interfaces.Queries
{
    public interface IQueryRssService : IQueryService<RssEntity>
    {
        Response<RssEntity> Get(long userId, string url);
        Response<IList<RssEntity>> GetList(long userId);
    }
}
