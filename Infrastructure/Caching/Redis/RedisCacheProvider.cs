using Core.Caching;
using StackExchange.Redis;
using System.Text.Json;

namespace Infrastructure.Caching.Redis
{
    public class RedisCacheProvider : ICacheProvider
    {
        private readonly IConnectionMultiplexer _redis;

        public RedisCacheProvider(IConnectionMultiplexer redis)
        {
            _redis = redis;
        }

        public async Task<T?> GetCacheValueAsync<T>(string key)
        {
            var db = _redis.GetDatabase();
            var value = await db.StringGetAsync(key);
            return value.IsNullOrEmpty ? default : JsonSerializer.Deserialize<T>(value);
        }

        public async Task SetCacheValueAsync<T>(string key, T value, TimeSpan expiration)
        {
            var db = _redis.GetDatabase();

            var json = JsonSerializer.Serialize(value);
            await db.StringSetAsync(key, json, expiration);
        }

        public async Task RemoveCacheAsync(string key)
        {
            var db = _redis.GetDatabase();
            await db.KeyDeleteAsync(key);
        }

        public async Task<bool> KeyExistsAsync(string key)
        {
            var db = _redis.GetDatabase();
            return await db.KeyExistsAsync(key);
        }

        public async Task<T?> GetOrSetCacheValueAsync<T, TIdentifier>(
            TIdentifier identifier,
            Func<Task<T>> factory,
            TimeSpan expiration,
            ICacheStrategy<TIdentifier> strategy)
        {
            var key = await strategy.GetKeyAsync(identifier);
            var cachedValue = await GetCacheValueAsync<T>(key);
            if (cachedValue != null)
            {
                return cachedValue;
            }

            var value = await factory();
            await SetCacheValueAsync(key, value, expiration);
            return value;
        }
        public async Task<IDictionary<string, T>> GetMultipleCacheValuesDictAsync<T>(IEnumerable<string> keys)
        {
            var db = _redis.GetDatabase();
            var tasks = keys.Select(key => db.StringGetAsync(key)).ToArray();
            var results = await Task.WhenAll(tasks);

            return keys.Zip(results, (key, value) => new { key, value })
                        .ToDictionary(x => x.key, x => !x.value.IsNullOrEmpty ? JsonSerializer.Deserialize<T>(x.value) : default);
        }
    }
}

