using System;
using System.Linq;
using VL.Common.Configurator.Objects.ConfigEntities;

namespace VL.Common.DAS.Utilities
{
    public class DbConfigHelper
    {
        static KSConfigEntity DbConnections;

        static DbConfigHelper()
        {
            DbConnections = new KSConfigEntity("DbConnections.config", "dbConnections", "dbConnection", "connectingString");
            if (!DbConnections.Load())
            {
                CreateConfigFile();
                throw new NotImplementedException("配置文件未存在,已创建文件,请调整配置后启动");
            }
        }

        public static void CreateConfigFile()
        {
            DbConnections = new KSConfigEntity("DbConnections.config", "dbConnections", "dbConnection", "connectingString");
            DbConnections.Items.Add(new KeyValueConfigEntity<string>("ConnectionName", "ConnectingString"));
            DbConnections.Save();
        }
        public static string GetDbConnectingString(string dbName)
        {
            var dbConnection = DbConnections.Items.FirstOrDefault(c => c.Key == dbName);
            if (dbConnection == null)
            {
                throw new NotImplementedException("未配置对应的数据库连接字符串" + dbName);
            }
            return dbConnection.Value;
        }
    }
}
