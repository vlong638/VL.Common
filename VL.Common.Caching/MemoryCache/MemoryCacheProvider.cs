using System;
using System.Collections.Specialized;
using System.Runtime.Caching;
using System.Threading.Tasks;

namespace VL.Common.Caching.MemoryCache
{
    public class MemoryCacheProvider : ICacheProvider
    {
        private System.Runtime.Caching.MemoryCache Cache { set; get; }

        public MemoryCacheProvider(string name = "Default", NameValueCollection config = null)
        {
            Cache = new System.Runtime.Caching.MemoryCache(name, config);
        }

        #region Get
        public T Get<T>(string key)
        {
            var value = Cache.Get(key);
            return value is T ? (T)value : default(T);
        }
        public Task<T> GetAsync<T>(string key)
        {
            return Task.Run(() => Get<T>(key));
        }
        #endregion

        #region Remove
        public void Remove(string key)
        {
            Cache.Remove(key);
        }
        public Task RemoveAsync(string key)
        {
            return Task.Run(() => Remove(key));
        }
        #endregion

        #region Set
        public void Set<T>(string key, T value, TimeSpan slidingExpiration)
        {
            Cache.Set(new CacheItem(key, value), new CacheItemPolicy() { SlidingExpiration = slidingExpiration });
        }
        public void Set<T>(string key, T value, DateTime absoluteExpiration)
        {
            Cache.Set(new CacheItem(key, value), new CacheItemPolicy() { AbsoluteExpiration = absoluteExpiration });
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
