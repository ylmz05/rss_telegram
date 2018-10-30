using Rss.CDO.Response;

namespace Rss.Application.Aggregates.Interfaces
{
    public interface ICommandAggregate<T> where T : class
    {
        Response<int> Add(T input);
        Response<int> Update(T input);
        Response<int> Remove(T input);
    }
}
