using System.Collections.Generic;
using VL.Common.Caching.Redis.Configs;

namespace VL.Common.Testing.Configs
{
    class RedisDbConfigEntity : DbConfigEntity
    {
        public RedisDbConfigEntity(string fileName = "RedisDbConnections") : base(fileName)
        {
        }

        protected override List<DbConfigItem> GetDbConfigItems()
        {
            return new List<DbConfigItem>() {
                new DbConfigItem("Default")
            };
        }
    }
}
