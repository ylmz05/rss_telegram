using Rss.Application.Services.Classes;
using Rss.CDO.Response;
using Rss.Domain.Entities;

namespace Rss.Application.Services.Interfaces.Commands
{
    public interface ICommandRssChatRelationService : ICommandService<RssChatRelationEntity>
    {
        Response<int> Add(RssChatRelationEntity input, string url);

        Response<int> Remove(long userId, string name, string url);
    }
}
