using System;
using System.IO;
using System.Text;
using VL.Common.Configurator.Objects.ConfigEntities;
using VL.Common.DAS.Utilities;
using VL.Common.Logger.Objects;

namespace VL.Common.Protocol.IService
{
    public abstract class ServiceContext
    {
        /// <summary>
        /// 内置日志记录器
        /// </summary>
        public static ILogger ServiceLogger { set; get; }
        /// <summary>
        /// 总协议配置
        /// </summary>
        public static ProtocolConfig ProtocolConfig { get; set; }
        /// <summary>
        /// 数据库配置
        /// CARE:如需分布式缓存数据库,也要按照数据库做相应连接配置
        /// </summary>
        public static DbConfigEntity DatabaseConfig { get; set; }

        public ServiceContext(DbConfigEntity databaseConfig, ProtocolConfig protocolConfig, ILogger serviceLogger)
        {
            DatabaseConfig = databaseConfig;
            ProtocolConfig = protocolConfig;
            ServiceLogger = serviceLogger;
        }

        #region Self Init()
        /// <summary>
        /// 上下文初始化
        /// </summary>
        /// <returns></returns>
        public bool InitForConsole()
        {
            ServiceLogger.Info("---------------------服务的依赖项检测--开始---------------------");
            DependencyResult result = new DependencyResult();
            //配置文件依赖检测
            result.DependencyDetails.Add(CheckAvailabilityOfConfigForService(ProtocolConfig));
            result.DependencyDetails.Add(CheckAvailabilityOfConfigForService(DatabaseConfig));
            //数据库依赖检测
            foreach (var dbConfigItem in DatabaseConfig.DbConfigItems)
            {
                result.DependencyDetails.Add(CheckAvailabilityOfDbSessionForService(dbConfigItem));
            }
            //其他依赖项检测
            foreach (var dependencyDetail in InitOthers().DependencyDetails)
            {
                result.DependencyDetails.Add(dependencyDetail);
            }
            //输出检测项结果
            foreach (var dependencyDetail in result.DependencyDetails)
            {
                Console.WriteLine(dependencyDetail.Message);
            }
            ServiceLogger.Info("---------------------服务的依赖项检测--结束---------------------");
            return result.IsAllDependenciesAvailable;
        }
        /// <summary>
        /// 校验配置文件的可用性:加载
        /// </summary>
        /// <param name="configEntity"></param>
        /// <returns></returns>
        private static bool CheckAvailabilityOfConfigForConsole(FileConfigEntity configEntity)
        {
            //文件不存在的辅助处理
            if (!File.Exists(configEntity.InputFilePath))
            {
                if (!Directory.Exists(configEntity.InputDirectoryPath))
                {
                    Directory.CreateDirectory(configEntity.InputDirectoryPath);
                }
                ServiceLogger.Error("配置文件不存在,配置文件:" + configEntity.InputFileName);
                try
                {
                    if (configEntity is DbConfigEntity)
                    {
                        DbConfigEntity dbConfig = (DbConfigEntity)configEntity;
                        dbConfig.DbConfigItems.Add(new DbConfigItem("OracleSample") { ConnectingString = "", DbType = DAS.Objects.EDatabaseType.Oracle });
                        dbConfig.DbConfigItems.Add(new DbConfigItem("MySQLSample") { ConnectingString = "", DbType = DAS.Objects.EDatabaseType.MySQL });
                        dbConfig.DbConfigItems.Add(new DbConfigItem("MSSQLSample") { ConnectingString = "", DbType = DAS.Objects.EDatabaseType.MSSQL });
                    }
                    configEntity.Save();
                    ServiceLogger.Error("已创建默认的配置文件,请在配置后重试,文件路径:" + configEntity.InputFilePath);
                }
                catch (Exception ex)
                {
                    ServiceLogger.Error("创建默认的配置文件失败,错误详情:" + ex.ToString());
                }
                return false;
            }
            else
            {
                //可用性检验
                try
                {
                    configEntity.Load();
                    ServiceLogger.Info("配置文件加载成功,配置文件:" + configEntity.InputFileName);
                    return true;
                }
                catch (Exception ex)
                {
                    ServiceLogger.Error("配置文件加载失败,配置文件:" + configEntity.InputFileName);
                    ServiceLogger.Error("错误详情" + ex.ToString());
                    return false;
                }
            }
        }
        /// <summary>
        /// 校验数据库的可用性:连接检测
        /// </summary>
        /// <param name="dbConfigItem"></param>
        /// <returns></returns>
        private static bool CheckAvailabilityOfDbSessionForConsole(DbConfigItem dbConfigItem)
        {
            try
            {
                var session = dbConfigItem.GetDbSession();
                if (session != null)
                {
                    session.Open();
                    ServiceLogger.Info("数据库连接成功,数据库配置名称:" + dbConfigItem.DbName);
                    session.Close();
                }
                return true;
            }
            catch (Exception ex)
            {
                ServiceLogger.Error("数据库连接失败,数据库配置名称:" + dbConfigItem.DbName);
                ServiceLogger.Error("错误详情" + ex.ToString());
                return false;
            }
        }
        #endregion
        #region Service Init()
        /// <summary>
        /// 上下文初始化
        /// </summary>
        /// <returns></returns>
        public DependencyResult InitForService()
        {
            ServiceLogger.Info("---------------------服务的依赖项检测--开始---------------------");
            DependencyResult result = new DependencyResult();
            //配置文件依赖检测
            result.DependencyDetails.Add(CheckAvailabilityOfConfigForService(ProtocolConfig));
            result.DependencyDetails.Add(CheckAvailabilityOfConfigForService(DatabaseConfig));
            //数据库依赖检测
            foreach (var dbConfigItem in DatabaseConfig.DbConfigItems)
            {
                result.DependencyDetails.Add(CheckAvailabilityOfDbSessionForService(dbConfigItem));
            }
            //其他依赖项检测
            foreach (var dependencyDetail in InitOthers().DependencyDetails)
            {
                result.DependencyDetails.Add(dependencyDetail);
            }
            ServiceLogger.Info("---------------------服务的依赖项检测--结束---------------------");
            return result;
        }
        /// <summary>
        /// 校验配置文件的可用性:加载
        /// TODO配置文件检测没必要每一次都重新加载
        /// </summary>
        /// <param name="configEntity"></param>
        /// <returns></returns>
        protected static DependencyDetail CheckAvailabilityOfConfigForService(FileConfigEntity configEntity)
        {
            DependencyDetail result = new DependencyDetail();
            result.DependencyType = DependencyType.Config;
            result.DependencyName = configEntity.InputFileName;
            StringBuilder message = new StringBuilder();
            //文件不存在的辅助处理
            if (!File.Exists(configEntity.InputFilePath))
            {
                if (!Directory.Exists(configEntity.InputDirectoryPath))
                {
                    Directory.CreateDirectory(configEntity.InputDirectoryPath);
                }
                message.AppendLine("配置文件不存在,配置文件:" + configEntity.InputFileName);
                try
                {
                    if (configEntity is DbConfigEntity)
                    {
                        DbConfigEntity dbConfig = (DbConfigEntity)configEntity;
                        dbConfig.DbConfigItems.Add(new DbConfigItem("OracleSample") { ConnectingString = "", DbType = DAS.Objects.EDatabaseType.Oracle });
                        dbConfig.DbConfigItems.Add(new DbConfigItem("MySQLSample") { ConnectingString = "", DbType = DAS.Objects.EDatabaseType.MySQL });
                        dbConfig.DbConfigItems.Add(new DbConfigItem("MSSQLSample") { ConnectingString = "", DbType = DAS.Objects.EDatabaseType.MSSQL });
                    }
                    configEntity.Save();
                    message.AppendLine("已创建默认的配置文件,请在配置后重试,文件路径:" + configEntity.InputFilePath);
                }
                catch (Exception ex)
                {
                    message.AppendLine("创建默认的配置文件失败,错误详情:" + ex.ToString());
                }
                result.IsDependencyAvailable = false;
            }
            else
            {
                //可用性检验
                try
                {
                    configEntity.Load();
                    message.AppendLine("配置文件加载成功,配置文件:" + configEntity.InputFileName);
                    result.IsDependencyAvailable = true;
                }
                catch (Exception ex)
                {
                    message.AppendLine("配置文件加载失败,配置文件:" + configEntity.InputFileName);
                    message.AppendLine("错误详情" + ex.ToString());
                    result.IsDependencyAvailable = false;
                }
            }
            result.Message = message.ToString();
            return result;
        }
        /// <summary>
        /// 校验数据库的可用性:连接检测
        /// </summary>
        /// <param name="dbConfigItem"></param>
        /// <returns></returns>
        protected static DependencyDetail CheckAvailabilityOfDbSessionForService(DbConfigItem dbConfigItem)
        {
            DependencyDetail result = new DependencyDetail();
            result.DependencyType = DependencyType.Database;
            result.DependencyName = dbConfigItem.DbName;
            StringBuilder message = new StringBuilder();

            try
            {
                var session = dbConfigItem.GetDbSession();
                if (session != null)
                {
                    session.Open();
                    message.AppendLine("数据库连接成功,数据库配置名称:" + dbConfigItem.DbName);
                    session.Close();
                }
                result.IsDependencyAvailable = true;
            }
            catch (Exception ex)
            {
                message.AppendLine("数据库连接失败,数据库配置名称:" + dbConfigItem.DbName);
                message.AppendLine("错误详情" + ex.ToString());
                result.IsDependencyAvailable = false;
            }
            result.Message = message.ToString();
            return result;
        }
        #endregion
        /// <summary>
        /// 上下文初始化
        /// </summary>
        /// <returns></returns>
        protected abstract DependencyResult InitOthers();
    }
}
