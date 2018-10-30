using Rss.CDO.Response;

namespace Rss.Application.Services.Classes
{
    public interface ICommandService<T>
    {
        Response<int> Add(T input);
        Response<int> Update(T input);
        Response<int> Remove(T input);
    }
}
