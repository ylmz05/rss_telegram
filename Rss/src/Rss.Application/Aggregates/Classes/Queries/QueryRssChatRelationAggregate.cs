using System.Collections.Generic;
using Rss.Application.Aggregates.Interfaces.Queries;
using Rss.CDO.Enums.Response;
using Rss.CDO.Response;
using Rss.Domain.Entities;
using Rss.Persistence.Repos.Interfaces.Queries;

namespace Rss.Application.Aggregates.Classes.Queries
{
    public class QueryRssChatRelationAggregate : IQueryRssChatRelationAggregate
    {
        private readonly IQueryRssCatRelationRepo _queryRssChatRepo;

        public QueryRssChatRelationAggregate(IQueryRssCatRelationRepo queryRssChatRepo)
        {
            _queryRssChatRepo = queryRssChatRepo;
        }

        public Response<RssChatRelationEntity> Get(int id)
        {
            return Response<RssChatRelationEntity>.Create(_queryRssChatRepo.Get(id), ResponseType.Success);
        }

        public Response<RssChatRelationEntity> Get(long userId, long chatId, string url)
        {
            return Response<RssChatRelationEntity>.Create(_queryRssChatRepo.Get(userId, chatId, url), ResponseType.Success);
        }

        public Response<RssChatRelationEntity> Get(long userId, string name, string url)
        {
            return Response<RssChatRelationEntity>.Create(_queryRssChatRepo.Get(userId, name, url), ResponseType.Success);
        }

        public Response<IList<RssChatRelationEntity>> GetList()
        {
            return Response<IList<RssChatRelationEntity>>.Create(_queryRssChatRepo.GetList(), ResponseType.Success);
        }

        public Response<IList<RssChatRelationEntity>> GetList(string AliasName)
        {
            return Response<IList<RssChatRelationEntity>>.Create(_queryRssChatRepo.GetList(AliasName), ResponseType.Success);
        }

        public Response<IList<RssChatRelationEntity>> GetList(long chatId)
        {
            return Response<IList<RssChatRelationEntity>>.Create(_queryRssChatRepo.GetList(chatId), ResponseType.Success);
        }

        public Response<IList<RssChatRelationEntity>> GetListByAliasName(long userId, string aliasName)
        {
            return Response<IList<RssChatRelationEntity>>.Create(_queryRssChatRepo.GetListByAliasName(userId, aliasName), ResponseType.Success);
        }

        public Response<IList<RssChatRelationEntity>> GetListByChatName(long userId, string chatName)
        {
            return Response<IList<RssChatRelationEntity>>.Create(_queryRssChatRepo.GetListByChatName(userId, chatName), ResponseType.Success);
        }
    }
}
