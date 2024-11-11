
namespace Core.Caching
{
    public interface ICacheProvider
    {
        Task<T> GetCacheValueAsync<T>(string key);
        Task SetCacheValueAsync<T>(string key, T value, TimeSpan expiration);
        Task RemoveCacheAsync(string key);
        Task<bool> KeyExistsAsync(string key);
        Task<T> GetOrSetCacheValueAsync<T, TIdentifier>(
            TIdentifier identifier,
            Func<Task<T>> factory,
            TimeSpan expiration,
            ICacheStrategy<TIdentifier> strategy);
        Task<IDictionary<string, T>> GetMultipleCacheValuesDictAsync<T>(IEnumerable<string> keys);
    }

}
