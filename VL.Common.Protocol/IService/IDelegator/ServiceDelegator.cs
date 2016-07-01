using System;
using VL.Common.DAS.Objects;

namespace VL.Common.Protocol.IService
{
    /// <summary>
    /// 用于规范服务的方法外框架(以隔离核心处理逻辑)
    /// </summary>
    public class ServiceDelegator
    {
        public ServiceContext ServiceContext { set; get; }

        public ServiceDelegator(ServiceContext serviceContext)
        {
            ServiceContext = serviceContext;
        }

        /// <summary>
        /// 整体异常处理
        /// ,日志处理
        /// .模拟
        /// </summary>
        public T HandleEvent<T>(Func<T> func, bool isSimulation = false) where T : Result, new()
        {
            var result = new T();
            result.MethodName = nameof(HandleEvent);
            result.ResultCode = EResultCode.Success;
            //业务逻辑处理
            try
            {
                result = func();
                //result.CopyAll(func());
            }
            catch (Exception ex)
            {
                result.ResultCode = EResultCode.Error;
                result.Message = ex.ToString();
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
        public T HandleSimpleTransactionEvent<T>(string dbName, Func<DbSession, T> func, bool isSimulation = false) where T : Result, new()
        {
            var result = new T();
            result.MethodName = nameof(HandleSimpleTransactionEvent);
            result.ResultCode = EResultCode.Success;
            DbSession session = null;
            //业务逻辑处理
            try
            {
                session = ServiceContext.DatabaseConfig.GetDbConfigItem(dbName).GetDbSession();
                session.Open();
                session.BeginTransaction();
                try
                {
                    result = func(session);
                    //result.CopyAll(func(session));
                }
                catch (Exception ex)
                {
                    result.ResultCode = EResultCode.Error;
                    result.Message = ex.ToString();
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
                result.Message = ex.ToString();
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
