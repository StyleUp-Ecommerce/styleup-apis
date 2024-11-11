

namespace Core.Caching
{
    public interface ICacheStrategy<T>
    {
        Task<string> GetKeyAsync(T identifier);
    }
}
