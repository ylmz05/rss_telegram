using System.Collections.Generic;
using Rss.Application.Aggregates.Interfaces.Queries;
using Rss.CDO.Enums.Response;
using Rss.CDO.Response;
using Rss.Domain.Entities;
using Rss.Persistence.Repos.Interfaces.Queries;

namespace Rss.Application.Aggregates.Classes.Queries
{
    public class QueryBotAggregate : IQueryBotAggregate
    {
        private readonly IQueryBotRepo _queryBotRepo;

        public QueryBotAggregate(IQueryBotRepo queryBotRepo)
        {
            _queryBotRepo = queryBotRepo;
        }

        public Response<BotEntity> Get(int id)
        {
            return Response<BotEntity>.Create(_queryBotRepo.Get(id), ResponseType.Success);
        }
        public Response<IList<BotEntity>> GetList()
        {
            return Response<IList<BotEntity>>.Create(_queryBotRepo.GetList(), ResponseType.Success);
        }
    }
}
