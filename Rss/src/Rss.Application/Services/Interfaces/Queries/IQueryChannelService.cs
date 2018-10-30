using Rss.Application.Services.Classes;
using Rss.CDO.Enums.Chat;
using Rss.CDO.Response;
using Rss.Domain.Entities;
using System.Collections.Generic;

namespace Rss.Application.Services.Interfaces.Queries
{
    public interface IQueryChannelService : IQueryService<ChannelEntity>
    {
        Response<IList<ChannelEntity>> GetList(ChatType type);
    }
}
