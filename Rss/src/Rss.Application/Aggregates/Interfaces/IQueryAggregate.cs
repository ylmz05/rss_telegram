using Rss.CDO.Response;
using System.Collections.Generic;

namespace Rss.Application.Aggregates.Interfaces
{
    public interface IQueryAggregate<T> where T : class
    {
        Response<T> Get(int id);
        Response<IList<T>> GetList();
    }
}
