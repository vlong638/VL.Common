using System;
using VL.Common.DAS.Objects;

namespace VL.Common.Protocol.IService//.IWCF
{
    /// <summary>
    /// 用于规范服务的方法外框架(以隔离核心处理逻辑)
    /// </summary>
    public class ServiceDelegator
    {
        public static ClassReportHelper ReportHelper = new ClassReportHelper(nameof(VL.Common.Protocol), nameof(ServiceDelegator));

        public ServiceDelegator(ServiceContext serviceContext)
        {
            ServiceContext = serviceContext;
        }
        public ServiceContext ServiceContext { set; get; }

        #region 0708之前的事务封装方法
        ///// <summary>
        ///// 整体异常处理
        ///// ,日志处理
        ///// .模拟
        ///// </summary>
        //public T HandleEvent<T>(Func<T> func, bool isSimulation = false) where T : Report, new()
        //{
        //    var result = new T();
        //    result.MethodName = nameof(HandleEvent);
        //    result.ResultCode = EResultCode.Success;
        //    //业务逻辑处理
        //    try
        //    {
        //        result = func();
        //        //result.CopyAll(func());
        //    }
        //    catch (Exception ex)
        //    {
        //        result.ResultCode = EResultCode.Error;
        //        result.Message = ex.ToString();
        //    }
        //    result.LogResult(ServiceContext.ServiceLogger);
        //    return result;
        //}
        ///// <summary>
        ///// 整体异常处理
        ///// ,日志处理
        ///// .单一会话+简单事务
        ///// .模拟
        ///// </summary>
        //public T HandleSimpleTransactionEvent<T>(string dbName, Func<DbSession, T> func, bool isSimulation = false) where T : Report, new()
        //{
        //    var result = new T();
        //    result.MethodName = nameof(HandleSimpleTransactionEvent);
        //    result.ResultCode = EResultCode.Success;
        //    DbSession session = null;
        //    //业务逻辑处理
        //    try
        //    {
        //        session = ServiceContext.DatabaseConfig.GetDbConfigItem(dbName).GetDbSession();
        //        session.Open();
        //        session.BeginTransaction();
        //        try
        //        {
        //            result = func(session);
        //        }
        //        catch (Exception ex)
        //        {
        //            result.ResultCode = EResultCode.Error;
        //            result.Message = ex.ToString();
        //        }
        //        if (result.ResultCode == EResultCode.Success)
        //        {
        //            session.CommitTransaction();
        //        }
        //        else
        //        {
        //            session.RollBackTransaction();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        result.ResultCode = EResultCode.Error;
        //        result.Message = ex.ToString();
        //    }
        //    finally
        //    {
        //        session.Close();
        //    }
        //    result.LogResult(ServiceContext.ServiceLogger);
        //    return result;
        //}
        #endregion

        #region 0708事务处理标准化方法
        /// <summary>
        /// 封装了事务处理的方法
        /// 只需专注于业务逻辑的实现
        /// </summary>
        public T HandleTransactionEvent<T>(DbSession session1, Func<DbSession, T> func) where T : Report, new()
        {
            //业务逻辑处理
            try
            {
                session1.Open();
                session1.BeginTransaction();
                T result;
                try
                {
                    result = func(session1);
                }
                catch (Exception ex)
                {
                    return (T)ReportHelper.GetReport(nameof(HandleTransactionEvent), Report.CodeOfError, ex.ToString());
                }
                if (result.Code == Report.CodeOfSuccess)
                {
                    session1.CommitTransaction();
                }
                else
                {
                    session1.RollBackTransaction();
                }
                return result;
            }
            catch (Exception ex)
            {
                if (session1 != null && session1.Connection.State == System.Data.ConnectionState.Open)
                {
                    session1.Close();
                }
                return (T)ReportHelper.GetReport(nameof(HandleTransactionEvent), Report.CodeOfError, ex.ToString());
            }
        }
        /// <summary>
        /// 封装了事务处理的方法
        /// 只需专注于业务逻辑的实现
        /// </summary>
        public T HandleTransactionEvent<T>(string session1Name, Func<DbSession, T> func) where T : Report, new()
        {
            try
            {
                return HandleTransactionEvent(ServiceContext.GetDbSession(session1Name), func);
            }
            catch (Exception ex)
            { 
                return (T)ReportHelper.GetReport(nameof(HandleTransactionEvent), Report.CodeOfError, ex.ToString());
            }    
        }
        /// <summary>
        /// 封装了事务处理的方法
        /// 只需专注于业务逻辑的实现
        /// </summary>HandleTransactionEvent
        public T HandleTransactionEvent<T>(DbSession session1, DbSession session2, Func<DbSession, DbSession, T> func) where T : Report, new()
        {
            //业务逻辑处理
            try
            {
                session1.Open();
                session2.Open();
                session1.BeginTransaction();
                session2.BeginTransaction();
                T result;
                try
                {
                    result = func(session1, session2);
                }
                catch (Exception ex)
                {
                    return (T)ReportHelper.GetReport(nameof(HandleTransactionEvent), Report.CodeOfError, ex.ToString());
                }
                if (result.Code == Report.CodeOfSuccess)
                {
                    session1.CommitTransaction();
                    session2.CommitTransaction();
                }
                else
                {
                    session1.RollBackTransaction();
                    session2.RollBackTransaction();
                }
                return result;
            }
            catch (Exception ex)
            {
                if (session1 != null && session1.Connection.State == System.Data.ConnectionState.Open)
                {
                    session1.Close();
                }
                if (session2 != null && session2.Connection.State == System.Data.ConnectionState.Open)
                {
                    session2.Close();
                }
                return (T)ReportHelper.GetReport(nameof(HandleTransactionEvent), Report.CodeOfError, ex.ToString());
            }
        }
        /// <summary>
        /// 封装了事务处理的方法
        /// 只需专注于业务逻辑的实现
        /// </summary>HandleTransactionEvent
        public T HandleTransactionEvent<T>(string session1Name, string session2Name, Func<DbSession, DbSession, T> func) where T : Report, new()
        {
            try
            {
                return HandleTransactionEvent(ServiceContext.GetDbSession(session1Name), ServiceContext.GetDbSession(session2Name), func);
            }
            catch (Exception ex)
            {
                return (T)ReportHelper.GetReport(nameof(HandleTransactionEvent), Report.CodeOfError, ex.ToString());
            }
        }
        #endregion
    }
}
