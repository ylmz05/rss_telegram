using Rss.Application.Aggregates.Interfaces.Commands;
using Rss.CDO.Enums.Response;
using Rss.CDO.Response;
using Rss.Domain.Entities;
using Rss.Persistence.Repos.Interfaces.Commands;

namespace Rss.Application.Aggregates.Classes.Commands
{
    public class CommandRssChatRelationAggregate : ICommandRssChatRelationAggregate
    {
        private readonly ICommandRssCatRelationRepo _commandRssChatRepo;

        public CommandRssChatRelationAggregate(ICommandRssCatRelationRepo commandRssChatRepo)
        {
            _commandRssChatRepo = commandRssChatRepo;
        }

        public Response<int> Add(RssChatRelationEntity input)
        {
            _commandRssChatRepo.Add(input);
            return Response<int>.Create(input.Id, ResponseType.Success);
        }
        public Response<int> Remove(RssChatRelationEntity input)
        {
            _commandRssChatRepo.Remove(input);
            return Response<int>.Create(input.Id, ResponseType.Success);
        }
        public Response<int> Update(RssChatRelationEntity input)
        {
            _commandRssChatRepo.Update(input);
            return Response<int>.Create(input.Id, ResponseType.Success);
        }
    }
}
