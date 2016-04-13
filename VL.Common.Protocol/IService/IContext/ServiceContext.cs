using System;
using System.IO;
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
        /// </summary>
        public static DbConfigEntity DatabaseConfig { get; set; }

        /// <summary>
        /// 上下文初始化
        /// </summary>
        /// <returns></returns>
        public bool Init() {
            bool result = true;
            ServiceLogger.Info("---------------------服务的依赖项检测--开始---------------------");
            //配置文件依赖检测
            result = result && CheckAvailabilityOfConfig(ProtocolConfig);
            result = result && CheckAvailabilityOfConfig(DatabaseConfig);
            //数据库依赖检测
            foreach (var dbConfigItem in DatabaseConfig.DbConfigItems)
            {
                result = result && CheckAvailabilityOfDbSession(dbConfigItem);
            }
            //其他依赖项检测
            result = result && InitOthers();
            ServiceLogger.Info("---------------------服务的依赖项检测--结束---------------------");
            return result;
        }
        /// <summary>
        /// 上下文初始化
        /// </summary>
        /// <returns></returns>
        public abstract bool InitOthers();
        /// <summary>
        /// 校验配置文件的可用性:加载
        /// </summary>
        /// <param name="configEntity"></param>
        /// <returns></returns>
        private static bool CheckAvailabilityOfConfig(FileConfigEntity configEntity)
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
                    configEntity.Save();
                    ServiceLogger.Error("已创建默认的配置文件,请在配置后重试,文件路径:" + configEntity.InputFilePath);
                }
                catch (Exception ex)
                {
                    ServiceLogger.Error("创建默认的配置文件失败,错误详情:" + ex.ToString());
                }
                return true;
            }
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
        /// <summary>
        /// 校验数据库的可用性:连接检测
        /// </summary>
        /// <param name="dbConfigItem"></param>
        /// <returns></returns>
        private static bool CheckAvailabilityOfDbSession(DbConfigItem dbConfigItem)
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
    }
}
