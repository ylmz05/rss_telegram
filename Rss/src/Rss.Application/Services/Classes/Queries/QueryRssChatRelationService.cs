using System.Collections.Generic;
using Rss.Application.Aggregates.Interfaces.Queries;
using Rss.Application.Services.Interfaces.Queries;
using Rss.CDO.Enums.Chat;
using Rss.CDO.Response;
using Rss.Domain.Entities;

namespace Rss.Application.Services.Classes.Queries
{
    public class QueryRssChatRelationService : IQueryRssChatRelationService
    {
        private readonly IQueryRssChatRelationAggregate _queryQueryAggregate = null;

        public QueryRssChatRelationService(IQueryRssChatRelationAggregate queryQueryAggregate)
        {
            _queryQueryAggregate = queryQueryAggregate;
        }

        public Response<RssChatRelationEntity> Get(int id)
        {
            return _queryQueryAggregate.Get(id);
        }
        public Response<IList<RssChatRelationEntity>> GetList()
        {
            return _queryQueryAggregate.GetList();
        }

        public Response<IList<RssChatRelationEntity>> GetList(string aliasName)
        {
            return _queryQueryAggregate.GetList(aliasName);
        }

        public Response<IList<RssChatRelationEntity>> GetList(long chatId)
        {
            return _queryQueryAggregate.GetList(chatId);
        }

        public Response<IList<RssChatRelationEntity>> GetList(long userId, string name, ListChatRelation type)
        {
            if (type.Equals(ListChatRelation.ByAliasName))
                return _queryQueryAggregate.GetListByAliasName(userId, name);
            else return _queryQueryAggregate.GetListByChatName(userId, name);
        }
    }
}
