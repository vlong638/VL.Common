using System;
using System.Linq;
using System.Runtime.Caching;
using VL.Common.Caching.MemoryCache;
using VL.Common.Caching.Redis;
using VL.Common.Logger.Utilities;
using VL.Common.Testing.Configs;

namespace VL.Common.Testing
{
    public static class StringEx
    {
        public static void ChangeString(this string s)
        {
            s = "2";
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            //var isSimulationAvailable = ProtocolConfig.IsSimulationAvailable;
            //TestCreateConfig();
            //TestLogger();

            //0422标准的测试
            //CreateRedisConfig();
            //MemoryCache();
            try
            {
                RedisCache();
            }
            catch (Exception ex)
            {
                string s = "";
            }
        }

        private static void RedisCache()
        {
            var config = new RedisDbConfigEntity();
            config.Load();
            var cache = new RedisCacheBuilder(config).GetInstance("Default");
            cache.Set("manager", "xiaForRedis", new TimeSpan(0, 0, 3) );
            var manager = cache.Get<string>("manager");
        }
        private static void MemoryCache()
        {
            var cache = new MemoryCacheBuilder().GetInstance("VL.MemoryCache");
            cache.Set("manager", "xiaForMemoryCache", new TimeSpan(0, 0, 3));
            var manager = cache.Get<string>("manager");
        }
        private static void CreateRedisConfig()
        {
            RedisDbConfigEntity config = new RedisDbConfigEntity();
            var configItem = config.DbConfigItems.First(c => c.DbName == "Default");
            configItem.DbNumber = 0;
            configItem.Host = "127.0.0.1";
            configItem.Port = 6379;
            config.Save();
        }
        private static void TextLoggerAndLog4Logger()
        {
            var log4Logger = LoggerProvider.GetLog4netLogger("Generator");
            log4Logger.Error("error");
            log4Logger.Info("info");
            var textLogger = LoggerProvider.GetTextLogger("text.config", System.Environment.CurrentDirectory + "/TextLogs");
            textLogger.Error("error2");
            textLogger.Info("info2");
            Console.ReadLine();
        }
        private static void TestCreateConfig()
        {
            //DbConfigHelper.CreateConfigFile();
        }
    }
}
