using Rss.Application.Aggregates.Interfaces.Commands;
using Rss.Application.Aggregates.Interfaces.Queries;
using Rss.Application.Services.Interfaces.Commands;
using Rss.CDO.Response;
using Rss.Domain.Entities;

namespace Rss.Application.Services.Classes.Commands
{
    public class CommandRssService : ICommandRssService
    {
        private readonly ICommandRssAggregate _commandRssAggregate = null;
        private readonly IQueryRssAggregate _queryRssAggregate = null;
        public CommandRssService(ICommandRssAggregate commandRssAggregate,
            IQueryRssAggregate queryRssAggregate)
        {
            _commandRssAggregate = commandRssAggregate;
            _queryRssAggregate = queryRssAggregate;
        }

        public Response<int> Add(RssEntity input)
        {
            Response<RssEntity> validation = _queryRssAggregate.Get(input.UserId, input.Url);
            if (validation.ResponseData != null)
                return Response<int>.Create(0, CDO.Enums.Response.ResponseType.AlreadyExist);
            return _commandRssAggregate.Add(input);
        }
        public Response<int> Remove(RssEntity input)
        {
            Response<RssEntity> validation = _queryRssAggregate.Get(input.UserId, input.Url);
            if (validation.ResponseData != null)
                return _commandRssAggregate.Remove(validation.ResponseData);
            else return Response<int>.Create(0, CDO.Enums.Response.ResponseType.NotFound);
        }
        public Response<int> Update(RssEntity input)
        {
            return _commandRssAggregate.Update(input);
        }
    }
}
