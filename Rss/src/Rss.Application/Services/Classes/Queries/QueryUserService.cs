using System.Collections.Generic;
using Rss.Application.Aggregates.Interfaces.Queries;
using Rss.Application.Services.Interfaces.Queries;
using Rss.CDO.Response;
using Rss.Domain.Entities;

namespace Rss.Application.Services.Classes.Queries
{
    public class QueryUserService : IQueryUserService
    {
        private readonly IQueryUserAggregate _queryUserAggregate = null;

        public QueryUserService(IQueryUserAggregate queryUserAggregate)
        {
            _queryUserAggregate = queryUserAggregate;
        }

        public Response<UserEntity> Get(int id)
        {
            return _queryUserAggregate.Get(id);
        }
        public Response<IList<UserEntity>> GetList()
        {
            return _queryUserAggregate.GetList();
        }
    }
}
