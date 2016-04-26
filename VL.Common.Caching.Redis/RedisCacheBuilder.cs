using System;
using System.Linq;
using VL.Common.Caching.Redis.Configs;

namespace VL.Common.Caching.Redis
{
    public class RedisCacheBuilder : ICacheBuilder
    {
        DbConfigEntity DbConfig { set; get; }

        public RedisCacheBuilder(DbConfigEntity config)
        {
            DbConfig = config;
        }

        protected RedisCacheBuilder()
        {
        }

        public override ICacheProvider GetInstance(string regionName)
        {
            var dbItem = DbConfig.DbConfigItems.FirstOrDefault(c => c.DbName == regionName);
            if (dbItem == null)
            {
                throw new NotImplementedException("未配置名称为:" + regionName + "的数据库");
            }
            return new RedisCacheProvider(dbItem);
        }
    }
}
