using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VL.Common.Caching.CacheProviders
{
    class TemplateProvider : ICacheProvider
    {
        public string Name
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        #region Get
        public T Get<T>(string key)
        {
            throw new NotImplementedException();
        }
        public Task<T> GetAsync<T>(string key)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Remove
        public void Remove(string key)
        {
            throw new NotImplementedException();
        }
        public Task RemoveAsync(string key)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Set
        public void Set<T>(string key, T value, TimeSpan slidingExpiration)
        {
            throw new NotImplementedException();
        }
        public void Set<T>(string key, T value, DateTime absoluteExpiration)
        {
            throw new NotImplementedException();
        }
        public Task SetAsync<T>(string key, T value, TimeSpan slidingExpiration)
        {
            throw new NotImplementedException();
        }
        public Task SetAsync<T>(string key, T value, DateTime absoluteExpiration)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
