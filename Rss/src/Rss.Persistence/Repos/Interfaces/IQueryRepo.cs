using Rss.Domain.EntityInterfaces;
using System.Collections.Generic;

namespace Rss.Persistence.Repos.Interfaces
{
    public interface IQueryRepo<T>
    {
        IList<T> GetList();

        T Get(int id);
    }
}
