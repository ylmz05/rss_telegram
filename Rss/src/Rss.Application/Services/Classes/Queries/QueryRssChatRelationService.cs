using System.Collections.Generic;
using Rss.Application.Aggregates.Interfaces.Queries;
using Rss.Application.Services.Interfaces.Queries;
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

        public Response<IList<RssChatRelationEntity>> GetList(string chatName)
        {
            return _queryQueryAggregate.GetList(chatName);
        }

        public Response<IList<RssChatRelationEntity>> GetList(long chatId)
        {
            return _queryQueryAggregate.GetList(chatId);
        }
    }
}
