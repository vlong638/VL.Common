using System;
using VL.Account.Business;
using VL.Common.Core.Object.VL.Account;
using VL.Common.Testing.CompositeTemplate;
using VL.Common.Testing.Utilities;
using static VL.Account.Business.TAccountDomain;

namespace VL.Common.Testing
{
    class Program
    {
        static void Main(string[] args)
        {
            #region 0810 ORM优化 增加对SQLite的支持
            //var connect= SQLiteHelper.Connect();
            //SQLiteHelper.PrepareTables();
            //TAccount account = new TAccount()
            //{
            //    AccountId = Guid.NewGuid(),
            //    AccountName = "vlong638",
            //    Password = "701616",
            //    CreatedOn = DateTime.Now,
            //    LastEditedOn = DateTime.Now,
            //};
            //TransactionHelper.HandleTransactionEvent(DbConfigs.DbNameOfAccount, (session) =>
            //{
            //    var selectResult = TAccountDomain.SelectAllAccounts(session);
            //    var createResult = account.Create(session);
            //});
            #endregion

            new MainNavigator().ShowMenuWithResult();

        }

        #region old
        //ProtocolConfig protocol = new ProtocolConfig("ProtocolConfig.config");
        //protocol.IsSQLLogAvailable.Value = true;
        //protocol.Save();
        //protocol.IsSQLLogAvailable.Value = false;
        //protocol.Load();
        //var isSimulationAvailable = ProtocolConfig.IsSimulationAvailable;
        //var isSimulationAvailable = ProtocolConfig.IsSimulationAvailable;
        //TestCreateConfig();
        //TestLogger();

        //0422标准的测试
        //CreateRedisConfig();
        //MemoryCache();
        //try
        //{
        //    RedisCache();
        //}
        //catch (Exception ex)
        //{
        //    string s = "";
        //}

        //private static void RedisCache()
        //{
        //    var config = new RedisDbConfigEntity();
        //    config.Load();
        //    var cache = new RedisCacheBuilder(config).GetInstance("Default");
        //    cache.Set("manager", "xiaForRedis", new TimeSpan(0, 0, 3));
        //    var manager = cache.Get<string>("manager");
        //}
        //private static void MemoryCache()
        //{
        //    var cache = new MemoryCacheBuilder().GetInstance("VL.MemoryCache");
        //    cache.Set("manager", "xiaForMemoryCache", new TimeSpan(0, 0, 3));
        //    var manager = cache.Get<string>("manager");
        //}
        //private static void CreateRedisConfig()
        //{
        //    RedisDbConfigEntity config = new RedisDbConfigEntity();
        //    var configItem = config.DbConfigItems.First(c => c.DbName == "Default");
        //    configItem.DbNumber = 0;
        //    configItem.Host = "127.0.0.1";
        //    configItem.Port = 6379;
        //    config.Save();
        //}
        //private static void TextLoggerAndLog4Logger()
        //{
        //    var log4Logger = LoggerProvider.GetLog4netLogger("Generator");
        //    log4Logger.Error("error");
        //    log4Logger.Info("info");
        //    var textLogger = LoggerProvider.GetTextLogger("text.config", System.Environment.CurrentDirectory + "/TextLogs");
        //    textLogger.Error("error2");
        //    textLogger.Info("info2");
        //    Console.ReadLine();
        //}
        //private static void TestCreateConfig()
        //{
        //    //DbConfigHelper.CreateConfigFile();
        //} 
        #endregion
    }
}
