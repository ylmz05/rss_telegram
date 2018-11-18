using Rss.Application.Services.Classes;
using Rss.CDO.Enums.Chat;
using Rss.CDO.Response;
using Rss.Domain.Entities;
using System.Collections.Generic;

namespace Rss.Application.Services.Interfaces.Queries
{
    public interface IQueryRssChatRelationService : IQueryService<RssChatRelationEntity>
    {
        Response<IList<RssChatRelationEntity>> GetList(string aliasName);
        Response<IList<RssChatRelationEntity>> GetList(long userId, string channelName, ListChatRelation type);
        Response<IList<RssChatRelationEntity>> GetList(long chatId);

    }
}
