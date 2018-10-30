using System.Collections.Generic;
using Rss.Application.Aggregates.Interfaces.Queries;
using Rss.Application.Services.Interfaces.Queries;
using Rss.CDO.Response;
using Rss.Domain.Entities;

namespace Rss.Application.Services.Classes.Queries
{
    public class QueryBotService : IQueryBotService
    {
        private readonly IQueryBotAggregate _queryBotAggregate = null;

        public QueryBotService(IQueryBotAggregate queryBotAggregate)
        {
            _queryBotAggregate = queryBotAggregate;
        }

        public Response<BotEntity> Get(int id)
        {
            return _queryBotAggregate.Get(id);
        }
        public Response<IList<BotEntity>> GetList()
        {
            return _queryBotAggregate.GetList();
        }
    }
}
