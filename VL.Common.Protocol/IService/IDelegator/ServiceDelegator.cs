using System;
using VL.Common.DAS.Objects;
using VL.Common.DAS.Utilities;

namespace VL.Common.Protocol.IService
{
    public class ServiceDelegator
    {
        /// <summary>
        /// 整体异常处理
        /// ,日志处理
        /// .模拟
        /// </summary>
        public static T HandleEvent<T>(Func<T> func, bool isSimulation = false) where T : Result, new()
        {
            var result = new T();
            result.ResultCode = EResultCode.Success;
            //模拟开关检测
            if (isSimulation&&!ServiceContext.ProtocolConfig.IsSimulationAvailable.Value)
            {
                result.ResultCode = EResultCode.Failure;
                result.Content = "未开启对Simulation的支持";
                return result;
            }
            //业务逻辑处理
            try
            {
                result = func();
            }
            catch (Exception ex)
            {
                result.ResultCode = EResultCode.Error;
                result.Content = ex.Message;
            }
            ServiceLogHelper.LogResult(result);
            return result;
        }
        /// <summary>
        /// 整体异常处理
        /// ,日志处理
        /// .单一会话+简单事务
        /// .模拟
        /// </summary>
        public static T HandleSimpleTransactionEvent<T>(string dbName, Func<DbSession, T> func, bool isSimulation = false) where T : Result, new()
        {
            var result = new T();
            result.ResultCode = EResultCode.Success;
            DbSession session = null;
            //模拟开关检测
            if (isSimulation && !ServiceContext.ProtocolConfig.IsSimulationAvailable.Value)
            {
                result.ResultCode = EResultCode.Failure;
                result.Content = "未开启对Simulation的支持";
                return result;
            }
            //业务逻辑处理
            try
            {
                session = ServiceContext.DatabaseConfig.GetDbConfigItem(dbName).GetDbSession();
                session.Open();
                session.BeginTransaction();
                try
                {
                    result = func(session);
                }
                catch (Exception ex)
                {
                    result.ResultCode = EResultCode.Error;
                    result.Content = ex.Message;
                }
                if (result.ResultCode == EResultCode.Success)
                {
                    session.CommitTransaction();
                }
                else
                {
                    session.RollBackTransaction();
                }
            }
            catch (Exception ex)
            {
                result.ResultCode = EResultCode.Error;
                result.Content = ex.Message;
            }
            finally
            {
                session.Close();
            }
            ServiceLogHelper.LogResult(result);
            return result;
        }
    }
}
