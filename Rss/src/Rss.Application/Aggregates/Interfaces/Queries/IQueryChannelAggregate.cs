using Rss.CDO.Enums.Chat;
using Rss.CDO.Response;
using Rss.Domain.Entities;
using System.Collections.Generic;

namespace Rss.Application.Aggregates.Interfaces.Queries
{
    public interface IQueryChannelAggregate : IQueryAggregate<ChannelEntity>
    {
        Response<ChannelEntity> Get(long userId, long chatId);
        Response<ChannelEntity> Get(long userId, string name);
        Response<IList<ChannelEntity>> GetList(ChatType type);
    }
}
