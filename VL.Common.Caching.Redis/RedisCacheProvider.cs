using ServiceStack.Redis;
using System;
using System.Threading.Tasks;
using VL.Common.Caching.Redis.Configs;

namespace VL.Common.Caching.Redis
{
    public class RedisCacheProvider : ICacheProvider
    {
        private RedisClient Client;
        private DbConfigItem Config;

        public RedisCacheProvider(DbConfigItem config)
        {
            Config = config;
            Client = new RedisClient(config.ToRedisEndpoint());
        }


        #region Get
        public T Get<T>(string key)
        {
            return Client.Get<T>(key);
        }
        public Task<T> GetAsync<T>(string key)
        {
            return Task.Run(() => Get<T>(key));
        }
        #endregion

        #region Remove
        public void Remove(string key)
        {
            Client.Remove(key);
        }
        public Task RemoveAsync(string key)
        {
            return Task.Run(() => Remove(key));
        }
        #endregion

        #region Set
        public void Set<T>(string key, T value, TimeSpan slidingExpiration)
        {
            Client.Set<T>(key, value, slidingExpiration);
        }
        public void Set<T>(string key, T value, DateTime absoluteExpiration)
        {
            Client.Set<T>(key, value, absoluteExpiration);
        }
        public Task SetAsync<T>(string key, T value, TimeSpan slidingExpiration)
        {
            return Task.Run(() => Set<T>(key, value, slidingExpiration));
        }
        public Task SetAsync<T>(string key, T value, DateTime absoluteExpiration)
        {
            return Task.Run(() => Set<T>(key, value, absoluteExpiration));
        }
        #endregion
    }
}
