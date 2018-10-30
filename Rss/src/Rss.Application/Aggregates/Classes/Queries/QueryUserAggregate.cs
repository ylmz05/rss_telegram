using Rss.Application.Aggregates.Interfaces.Queries;
using Rss.CDO.Enums.Response;
using Rss.CDO.Response;
using Rss.Domain.Entities;
using Rss.Persistence.Repos.Interfaces.Queries;
using System.Collections.Generic;

namespace Rss.Application.Aggregates.Classes.Queries
{
    public class QueryUserAggregate : IQueryUserAggregate
    {
        private readonly IQueryUserRepo _queryUserRepo;

        public QueryUserAggregate(IQueryUserRepo queryUserRepo)
        {
            _queryUserRepo = queryUserRepo;
        }

        public Response<UserEntity> Get(int id)
        {
            return Response<UserEntity>.Create(_queryUserRepo.Get(id), ResponseType.Success);
        }
        public Response<IList<UserEntity>> GetList()
        {
            return Response<IList<UserEntity>>.Create(_queryUserRepo.GetList(), ResponseType.Success);
        }
    }
}
