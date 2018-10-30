using Rss.Application.Aggregates.Interfaces.Commands;
using Rss.CDO.Enums.Response;
using Rss.CDO.Response;
using Rss.Domain.Entities;
using Rss.Persistence.Repos.Interfaces.Commands;

namespace Rss.Application.Aggregates.Classes.Commands
{
    public class CommandUserAggregate : ICommandUserAggregate
    {
        private readonly ICommandUserRepo _commandUserRepo;

        public CommandUserAggregate(ICommandUserRepo commandUserRepo)
        {
            _commandUserRepo = commandUserRepo;
        }

        public Response<int> Add(UserEntity input)
        {
            _commandUserRepo.Add(input);
            return Response<int>.Create(input.Id, ResponseType.Success);
        }
        public Response<int> Remove(UserEntity input)
        {
            _commandUserRepo.Remove(input);
            return Response<int>.Create(input.Id, ResponseType.Success);
        }
        public Response<int> Update(UserEntity input)
        {
            _commandUserRepo.Update(input);
            return Response<int>.Create(input.Id, ResponseType.Success);
        }
    }
}
