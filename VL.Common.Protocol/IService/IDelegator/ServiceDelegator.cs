using System;
using VL.Common.DAS.Objects;

namespace VL.Common.Protocol.IService
{
    /// <summary>
    /// 用于规范服务的方法外框架(以隔离核心处理逻辑)
    /// </summary>
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
            result.MethodName = nameof(HandleEvent);
            result.ResultCode = EResultCode.Success;
            //模拟开关检测
            if (isSimulation && !ServiceContext.ProtocolConfig.IsSimulationAvailable.Value)
            {
                result.ResultCode = EResultCode.Failure;
                result.Message = "未开启对Simulation的支持";
                return result;
            }
            //业务逻辑处理
            try
            {
                result.CopyAll(func());
            }
            catch (Exception ex)
            {
                result.ResultCode = EResultCode.Error;
                result.Message = ex.Message;
            }
            result.LogResult(ServiceContext.ServiceLogger);
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
            result.MethodName = nameof(HandleSimpleTransactionEvent);
            result.ResultCode = EResultCode.Success;
            DbSession session = null;
            //模拟开关检测
            if (isSimulation && !ServiceContext.ProtocolConfig.IsSimulationAvailable.Value)
            {
                result.ResultCode = EResultCode.Failure;
                result.Message = "未开启对Simulation的支持";
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
                    result.CopyAll(func(session));
                }
                catch (Exception ex)
                {
                    result.ResultCode = EResultCode.Error;
                    result.Message = ex.Message;
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
                result.Message = ex.Message;
            }
            finally
            {
                session.Close();
            }
            result.LogResult(ServiceContext.ServiceLogger);
            return result;
        }
    }
}
