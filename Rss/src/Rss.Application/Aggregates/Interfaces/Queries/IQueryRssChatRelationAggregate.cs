﻿using Rss.CDO.Response;
using Rss.Domain.Entities;
using System.Collections.Generic;

namespace Rss.Application.Aggregates.Interfaces.Queries
{
    public interface IQueryRssChatRelationAggregate : IQueryAggregate<RssChatRelationEntity>
    {
        Response<RssChatRelationEntity> Get(long userId, long chatId, string url);
        Response<RssChatRelationEntity> Get(long userId, string name, string url);
        Response<IList<RssChatRelationEntity>> GetList(string aliasName);
        Response<IList<RssChatRelationEntity>> GetListByAliasName(long userId, string name);
        Response<IList<RssChatRelationEntity>> GetList(long chatId);
        Response<IList<RssChatRelationEntity>> GetListByChatName(long userId, string name);
    }
}
