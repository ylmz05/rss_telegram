using Rss.CDO.Response;
using Rss.Domain.Entities;
using System.Collections.Generic;

namespace Rss.Persistence.Repos.Interfaces.Queries
{
    public interface IQueryChannelRepo : IQueryRepo<ChannelEntity>
    {
        ChannelEntity Get(long userId, long chatId);
        ChannelEntity Get(long userId, string name);
        IList<ChannelEntity> GetList(int type);
        IList<ChannelEntity> GetList(long userId);
        IList<ChannelEntity> GetList(long userId, int chatType);
    }
}
