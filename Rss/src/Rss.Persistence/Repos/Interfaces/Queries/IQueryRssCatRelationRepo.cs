﻿using Rss.Domain.Entities;
using System.Collections.Generic;

namespace Rss.Persistence.Repos.Interfaces.Queries
{
    public interface IQueryRssCatRelationRepo : IQueryRepo<RssChatRelationEntity>
    {
        RssChatRelationEntity Get(long userId, long chatId, string url);
        RssChatRelationEntity Get(long userId, string name, string url);
        IList<RssChatRelationEntity> GetList(string aliasName);
        IList<RssChatRelationEntity> GetListByAliasName(long userId, string aliasName);
        IList<RssChatRelationEntity> GetList(long chatId);
        IList<RssChatRelationEntity> GetListByChatName(long userId, string chatName);
    }
}
