using Rss.Application.Services.Classes;
using Rss.CDO.Response;
using Rss.Domain.Entities;
using System.Collections.Generic;

namespace Rss.Application.Services.Interfaces.Queries
{
    public interface IQueryRssChatRelationService : IQueryService<RssChatRelationEntity>
    {
        Response<IList<RssChatRelationEntity>> GetList(string chatName);
        Response<IList<RssChatRelationEntity>> GetList(long chatId);

    }
}
