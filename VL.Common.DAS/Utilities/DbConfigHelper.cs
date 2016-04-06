using System;
using System.Linq;
using VL.Common.Configurator.Objects.ConfigEntities;
using VL.Common.DAS.Objects;

namespace VL.Common.DAS.Utilities
{
    public class DbConfigHelper
    {
        static KSSConfigEntity DbConnections;

        static DbConfigHelper()
        {
            DbConnections = new KSSConfigEntity("DbConnections.config", "dbConnections", "dbConnection", "connectingString", "dbType");
            if (!DbConnections.Load())
            {
                CreateConfigFile();
                throw new NotImplementedException("配置文件未存在,已创建文件,请调整配置后启动");
            }
        }

        public static void CreateConfigFile()
        {
            DbConnections = new KSSConfigEntity("DbConnections.config", "dbConnections", "dbConnection", "connectingString", "dbType");
            DbConnections.Items.Add(new KeyValueConfigEntity<string, string>("ConnectionName", "ConnectingString", EDatabaseType.None.ToString()));
            DbConnections.Save();
        }
        public static string GetDbConnectingString(string dbName)
        {
            var dbConnection = DbConnections.Items.FirstOrDefault(c => c.Key == dbName);
            if (dbConnection == null)
            {
                throw new NotImplementedException("未配置对应的数据库连接字符串" + dbName);
            }
            return dbConnection.Value1;
        }
        public static EDatabaseType GetDbType(string dbName)
        {
            var dbConnection = DbConnections.Items.FirstOrDefault(c => c.Key == dbName);
            if (dbConnection == null)
            {
                throw new NotImplementedException("未配置对应的数据库连接字符串" + dbName);
            }
            return (EDatabaseType)Enum.Parse(typeof(EDatabaseType), dbConnection.Value2);
        }
    }
}
