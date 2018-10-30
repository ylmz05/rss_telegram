namespace Rss.Persistence.Repos.Interfaces
{
    public interface ICommandRepo<T>
    {
        void Add(T input);
        void Update(T input);
        void Remove(T input);
    }
}
