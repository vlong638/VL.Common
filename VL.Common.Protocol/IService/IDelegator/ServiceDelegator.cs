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

        #region 0708之前的事务封装方法
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
        #endregion

        #region 0708事务处理标准化方法
        public T HandleTransactionEvent<T>(DbSession session1, Func<DbSession, T> func) where T : Result, new()
        {
            var result = new T();
            result.MethodName = nameof(HandleTransactionEvent);
            result.ResultCode = EResultCode.Success;
            //业务逻辑处理
            try
            {
                session1.Open();
                session1.BeginTransaction();
                try
                {
                    result = func(session1);
                }
                catch (Exception ex)
                {
                    result.ResultCode = EResultCode.Error;
                    result.Message = ex.ToString();
                }
                if (result.ResultCode == EResultCode.Success)
                {
                    session1.CommitTransaction();
                }
                else
                {
                    session1.RollBackTransaction();
                }
            }
            catch (Exception ex)
            {
                result.ResultCode = EResultCode.Error;
                result.Message = ex.ToString();
            }
            finally
            {
                if (session1 != null && session1.Connection.State == System.Data.ConnectionState.Open)
                {
                    session1.Close();
                }
            }
            result.LogResult(ServiceContext.ServiceLogger);
            return result;
        }
        public T HandleTransactionEvent<T>(DbSession session1, DbSession session2, Func<DbSession, DbSession, T> func) where T : Result, new()
        {
            var result = new T();
            result.MethodName = nameof(HandleTransactionEvent);
            result.ResultCode = EResultCode.Success;
            //业务逻辑处理
            try
            {
                session1.Open();
                session2.Open();
                session1.BeginTransaction();
                session2.BeginTransaction();
                try
                {
                    result = func(session1, session2);
                }
                catch (Exception ex)
                {
                    result.ResultCode = EResultCode.Error;
                    result.Message = ex.ToString();
                }
                if (result.ResultCode == EResultCode.Success)
                {
                    session1.CommitTransaction();
                    session2.CommitTransaction();
                }
                else
                {
                    session1.RollBackTransaction();
                    session2.RollBackTransaction();
                }
            }
            catch (Exception ex)
            {
                result.ResultCode = EResultCode.Error;
                result.Message = ex.ToString();
            }
            finally
            {
                if (session1 != null && session1.Connection.State == System.Data.ConnectionState.Open)
                {
                    session1.Close();
                }
                if (session2 != null && session2.Connection.State == System.Data.ConnectionState.Open)
                {
                    session2.Close();
                }
            }
            result.LogResult(ServiceContext.ServiceLogger);
            return result;
        }
        public T HandleTransactionEvent<T>(string session1Name, Func<DbSession, T> func) where T : Result, new()
        {
            var result = new T();
            result.MethodName = nameof(HandleTransactionEvent);
            result.ResultCode = EResultCode.Success;
            DbSession session1 = null;
            //业务逻辑处理
            try
            {
                session1 = ServiceContext.DatabaseConfig.GetDbConfigItem(session1Name).GetDbSession();
                session1.Open();
                session1.BeginTransaction();
                try
                {
                    result = func(session1);
                }
                catch (Exception ex)
                {
                    result.ResultCode = EResultCode.Error;
                    result.Message = ex.ToString();
                }
                if (result.ResultCode == EResultCode.Success)
                {
                    session1.CommitTransaction();
                }
                else
                {
                    session1.RollBackTransaction();
                }
            }
            catch (Exception ex)
            {
                result.ResultCode = EResultCode.Error;
                result.Message = ex.ToString();
            }
            finally
            {
                if (session1 != null && session1.Connection.State == System.Data.ConnectionState.Open)
                {
                    session1.Close();
                }
            }
            result.LogResult(ServiceContext.ServiceLogger);
            return result;
        }
        public T HandleTransactionEvent<T>(string session1Name, string session2Name, Func<DbSession, DbSession, T> func) where T : Result, new()
        {
            var result = new T();
            result.MethodName = nameof(HandleTransactionEvent);
            result.ResultCode = EResultCode.Success;
            DbSession session1 = null;
            DbSession session2 = null;
            //业务逻辑处理
            try
            {
                session1 = ServiceContext.DatabaseConfig.GetDbConfigItem(session1Name).GetDbSession();
                session2 = ServiceContext.DatabaseConfig.GetDbConfigItem(session2Name).GetDbSession();
                session1.Open();
                session2.Open();
                session1.BeginTransaction();
                session2.BeginTransaction();
                try
                {
                    result = func(session1, session2);
                }
                catch (Exception ex)
                {
                    result.ResultCode = EResultCode.Error;
                    result.Message = ex.ToString();
                }
                if (result.ResultCode == EResultCode.Success)
                {
                    session1.CommitTransaction();
                    session2.CommitTransaction();
                }
                else
                {
                    session1.RollBackTransaction();
                    session2.RollBackTransaction();
                }
            }
            catch (Exception ex)
            {
                result.ResultCode = EResultCode.Error;
                result.Message = ex.ToString();
            }
            finally
            {
                if (session1 != null && session1.Connection.State == System.Data.ConnectionState.Open)
                {
                    session1.Close();
                }
                if (session2 != null && session2.Connection.State == System.Data.ConnectionState.Open)
                {
                    session2.Close();
                }
            }
            result.LogResult(ServiceContext.ServiceLogger);
            return result;
        }
        #endregion
    }
}
