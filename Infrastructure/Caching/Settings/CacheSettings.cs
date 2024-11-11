

namespace Infrastructure.Caching.Setting
{
    public class CacheSettings
    {
        public string ConnectionString { get; set; }
        public string InstanceName { get; set; }
        public TimeSpan DefaultExpiration { get; set; } = TimeSpan.FromMinutes(30);
    }

}
