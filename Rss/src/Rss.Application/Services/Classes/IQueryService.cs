using Rss.CDO.Response;
using System.Collections.Generic;

namespace Rss.Application.Services.Classes
{
    public interface IQueryService<T>
    {
        Response<T> Get(int id);
        Response<IList<T>> GetList();
    }
}
